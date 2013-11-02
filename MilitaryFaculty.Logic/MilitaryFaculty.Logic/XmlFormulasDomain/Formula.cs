using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlFormulasDomain
{
    [Serializable]
    public class Formula
    {
        #region Class Properties

        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("expression")]
        public string Expression { get; set; }
        public List<Argument> Arguments { get; set; }
        public List<Coefficient> Coefficients { get; set; }

        #endregion // Class Properties

        #region Class Public Methods

        public FormulaInfo ToFormulaInfo()
        {
            return new FormulaInfo
            {
                Coefficients = this.Coefficients.ToDictionary(c => c.Name,
                                                              c => c.Value),
                Arguments = this.Arguments.Select(a => a.Name).ToList(),
                Expression = this.Expression
            };
        }

        #endregion // Class Public Methods
    }
}
