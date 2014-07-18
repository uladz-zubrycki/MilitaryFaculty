using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MilitaryFaculty.FormulaEditor.Domain
{
    /// <summary>
    /// Represents in-memory formulas-document.
    /// </summary>
    internal sealed class FormulaDocument
    {
        public readonly string FilePath;
        public readonly IReadOnlyCollection<Formula> Formulas;

        /// <summary>
        /// Creates new instance of <see cref="FormulaDocument"/> using provided parameters.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="formulas"></param>
        public FormulaDocument(string filePath, IEnumerable<Formula> formulas)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }
            if (formulas == null)
            {
                throw new ArgumentNullException("formulas");
            }

            FilePath = filePath;
            Formulas = formulas.ToImmutableList();
        }
    }
}