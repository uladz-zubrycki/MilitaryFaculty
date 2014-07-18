using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.FormulaEditor.Domain;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.FormulaEditor
{
    internal sealed class FormulaService
    {
        public FormulaDocument Load(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException();
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(("Can't find file at path " +
                                                 "'{0}'").f(filePath));
            }

            using (var stream = File.OpenRead(filePath))
            {
                var serializer = new XmlSerializer(typeof (List<XFormula>));
                var xFormulas = serializer.Deserialize<List<XFormula>>(stream);
                var formulas = xFormulas.Select(FormulaXmlHelper.FromXml);

                return new FormulaDocument(filePath,
                                           formulas);
            }
        }

        public void Save(FormulaDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            if (File.Exists(document.FilePath))
            {
                BackupOldFile(document.FilePath);
                DeleteOldFile(document.FilePath);
            }

            SaveDocument(document);
        }

        private static void SaveDocument(FormulaDocument document)
        {
            var errors = new List<Exception>();
            Action<Exception> onError = errors.Add;

            try
            {
                var formulas = document.Formulas
                                       .Select(FormulaXmlHelper.ToXml)
                                       .ToList();

                using (var writer = File.CreateText(document.FilePath))
                {
                    var serializer = new XmlSerializer(typeof (List<XFormula>));
                    serializer.Serialize(writer, formulas);
                }
            }
            catch (Exception ex)
            {
                onError(ex);
                Invoke(action: () => RestoreBackup(document.FilePath),
                       onError: onError);
            }
            finally
            {
                Invoke(action: () => DeleteBackup(document.FilePath),
                       onError: onError);
            }

            if (errors.Any())
            {
                throw new AggregateException(errors);
            }
        }

        private static void BackupOldFile(string filePath)
        {
            var backupName = CreateBackupPath(filePath);

            if (File.Exists(backupName))
            {
                File.Delete(backupName);
            }

            File.Copy(filePath, backupName);
        }

        private static void DeleteOldFile(string filePath)
        {
            File.Delete(filePath);
        }

        private static void DeleteBackup(string filePath)
        {
            Invoke(action: () => File.Delete(filePath),
                   throwOnExceptions: false);
        }

        private static void RestoreBackup(string filePath)
        {
            var backupPath = CreateBackupPath(filePath);

            if (!File.Exists(backupPath))
            {
                throw new FileNotFoundException(("Can't restore file previous version, " +
                                                 "no backup at '{0}'").f(backupPath));
            }

            File.Move(backupPath, filePath);
        }

        private static string CreateBackupPath(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileName(filePath);
            var extension = Path.GetExtension(filePath);
            var backupName = "{0}-old{1}".f(fileName, extension);
            var backupPath = Path.Combine(directory, backupName);

            return backupPath;
        }

        /// <summary>
        /// Invokes specified action.
        /// </summary>
        /// <param name="action">Action to invoke.</param>
        /// <param name="throwOnExceptions">
        /// Controls exception handling. 
        /// Rethrow if true; otherwise swallow.
        /// </param>
        private static void Invoke(Action action, bool throwOnExceptions)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            try
            {
                action();
            }
            catch
            {
                if (throwOnExceptions)
                {
                    throw;
                }

                // Logger.InfoException("", ex);
            }
        }

        /// <summary>
        /// Invokes specified action.
        /// </summary>
        /// <param name="action">Action to invoke.</param>
        /// <param name="onError">Action to perform, if exception arises.</param>
        private static void Invoke(Action action, Action<Exception> onError)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (onError == null)
            {
                throw new ArgumentNullException("onError");
            }

            try
            {
                action();
            }
            catch (Exception ex)
            {
                onError(ex);
            }
        }
    }
}