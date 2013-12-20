using System.Linq;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;
using NUnit.Framework;

namespace MilitaryFaculty.Presentation.Tests
{
    [TestFixture]
    public class EntityViewModelTest
    {
        private class DummyEntityViewModel : EntityViewModel<Professor>
        {
            public DummyEntityViewModel(Professor model) 
                : base(model)
            {
                // Empty
            }

            [TextProperty]
            public JobPosition JobPosition
            {
                get { return Model.JobPosition; }
                set
                {
                    SetModelProperty(m => m.JobPosition, value);
                }
            }

            [TextProperty]
            public AcademicDegree AcademicDegree
            {
                get { return Model.AcademicDegree; }
                set
                {
                    SetModelProperty(m => m.AcademicDegree, value);
                }
            }

            [TextProperty(Label = "Academic rank")]
            public AcademicRank AcademicRank
            {
                get { return Model.AcademicRank; }
                set
                {
                    SetModelProperty(m => m.AcademicRank, value);
                }
            }
        }

        [Test]
        public void GetProperties_NamesAsExpected()
        {
            var expectedNames = new []
                                {
                                    "JobPosition",
                                    "AcademicDegree",
                                    "Academic rank"
                                };

            var target = new DummyEntityViewModel(new Professor());
            var names = target.Properties
                              .Select(p => p.Label)
                              .ToArray();

            Assert.AreEqual(expectedNames, names);
        }

        [Test]
        public void SetPropertyValue_RightResult()
        {
            const JobPosition value = JobPosition.HeadOfCathedra;

            var target = new DummyEntityViewModel(new Professor());
            var viewModel = target.Properties.First();
            viewModel.Property = value;

            Assert.AreEqual(value, viewModel.Property);
        }
    }
}
