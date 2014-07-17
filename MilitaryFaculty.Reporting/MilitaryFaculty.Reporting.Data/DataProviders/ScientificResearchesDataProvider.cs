﻿using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ScientificResearchesDataProvider : DataProvider<ScientificResearch>
    {
        public ScientificResearchesDataProvider(IRepository<ScientificResearch> repository)
            : base(repository)
        {
        }

        public ScientificResearchesDataProvider(IRepository<ScientificResearch> repository,
            Expression<Func<ScientificResearch, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     Количество выполненных НИР
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SrwCount")]
        public double ScientificResearchWorksCount()
        {
            return CountOf(w => true);
        }

        /// <summary>
        ///     Количество проведенных военно-научных сопровождений
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MssCount")]
        public double MilitaryScientificSupportsCount()
        {
            return CountOf(w => w.State == MilitaryScientificSupportState.Completed);
        }
    }
}