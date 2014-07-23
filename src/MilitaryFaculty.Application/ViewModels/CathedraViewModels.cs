using System.Collections.Generic;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class CathedraView
    {
        internal class Root : EntityRootViewModel<Cathedra>
        {
            public Root(Cathedra model) : base(model) { }

            protected override IEnumerable<ViewModel<Cathedra>> GetViewModels()
            {
                return new[]
                       {
                           new MainInfo(Model)
                       };
            }
        }

        internal class MainInfo : ViewModel<Cathedra>
        {
            public MainInfo(Cathedra model): base(model) { }

            public override string Title
            {
                get { return Model.Name; }
            }
        }
    }
}