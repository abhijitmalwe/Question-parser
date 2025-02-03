using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Xengine.Admin.Models;
using Xengine.DAL;

namespace Xengine.Admin.Core
{
    public interface IDbManager
    {
        void Save(ExamEntity exam, DocxFile examFile, BackgroundWorker backgroundWorker);
    }

    public class DbManager : IDbManager
    {
        private readonly ILogger<DbManager> _logger;
        private readonly AppSettings _appSettings;

        private BackgroundWorker _backgroundWorker;

        public DbManager(ILogger<DbManager> log, AppSettings appSettings)
        {
            _logger = log;
            _appSettings = appSettings;
        }

        public void Save(ExamEntity exam, DocxFile examFile, BackgroundWorker backgroundWorker)
        {
            _logger.LogDebug("Inside Save() function.");
            _logger.LogDebug($"{_appSettings.SqLiteConnectionString}");
            _logger.LogDebug($"{_appSettings.DbPassword}");

            IRepositoryWrapper dbRepo = new RepositoryWrapper(_appSettings.SqLiteConnectionString, _appSettings.DbPassword);
            try
            {
                _backgroundWorker = backgroundWorker;

                //Cleanup the DB
                ResetDefaultDatabase(dbRepo);

                //Sort the questions
               // exam.Questions = exam.Questions.OrderBy(q => int.Parse(q.Number.RemoveQuestionLabel())).ToList();

                dbRepo.Exam.AddWithChildAsync(exam);
                
                CopySqliteDatabase(examFile.ExmFilePath);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}");
            }
            finally
            {
                ResetDefaultDatabase(dbRepo);
            }
        }

        private void CopySqliteDatabase(string fullDbPath)
        {
            var fileInfo = new FileInfo(_appSettings.ConnectionString);
            File.Copy(fileInfo.FullName, fullDbPath, true);
        }

        private void ResetDefaultDatabase(IRepositoryWrapper dbRepo)
        {
            dbRepo.Exam.RunSql("delete from exam");
            dbRepo.Exam.RunSql("delete from sqlite_sequence");
            dbRepo.Exam.RunSql("VACUUM;");
        }
    }
}