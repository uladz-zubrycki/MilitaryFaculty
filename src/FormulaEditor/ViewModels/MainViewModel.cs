using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.FormulaEditor.Domain;
using MilitaryFaculty.Presentation.Core.ViewModels;

namespace MilitaryFaculty.FormulaEditor.ViewModels
{
    internal class MainViewModel
    {

    }

    internal class FormulaViewModel : ViewModel
    {
        public string Name { get; set; }
        public double MaxValue { get; set; }
        public string Expression { get; set; }
        public IEnumerable<ArgumentViewModel> Arguments;
        public IEnumerable<CoefficientViewModel> Coefficients;

        public static FormulaViewModel FromModel(Formula model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            var arguments = model.Arguments
                                 .Select(ArgumentViewModel.FromModel)
                                 .ToList();

            var coefficients = model.Coefficients
                                    .Select(CoefficientViewModel.FromModel)
                                    .ToList();

            return new FormulaViewModel
            {
                Name = model.Name,
                MaxValue = model.MaxValue,
                Expression = model.Expression,
                Arguments = arguments,
                Coefficients = coefficients
            };
        }
    }

    internal class ArgumentViewModel : ViewModel
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public static ArgumentViewModel FromModel(Argument model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            return new ArgumentViewModel
                   {
                       Name = model.Name,
                       Text = model.Text
                   };
        }
    }

    internal class CoefficientViewModel : ViewModel
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public static CoefficientViewModel FromModel(Coefficient model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            return new CoefficientViewModel
                   {
                       Name = model.Name,
                       Value = model.Value
                   };
        }
    }
}