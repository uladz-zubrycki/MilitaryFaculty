using System;

namespace MilitaryFaculty.FormulaEditor.Domain
{
    internal class Argument
    {
        public readonly string Name;
        public readonly string Text;

        public Argument(string name, string text)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            Name = name;
            Text = text;
        }
    }
}