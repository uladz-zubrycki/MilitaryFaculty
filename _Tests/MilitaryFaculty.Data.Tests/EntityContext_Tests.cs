using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MilitaryFaculty.Domain;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace MilitaryFaculty.Data.Tests
{
    [TestFixture]
    public class EntityContext_Tests
    {
        private EntityContext context;

        [SetUp]
        public void SetUp()
        {
            const string conName = "Current";

            var connectionString = ConfigurationManager.ConnectionStrings[conName]
                .ConnectionString;

            Database.SetInitializer(new DropCreateDatabaseAlways<EntityContext>());
            context = new EntityContext(connectionString);
        }

        [Test]
        public void InitDatabase()
        {
            SeedCathedras(context);
            SeedProfessors(context);
            SeedConferences(context);
            SeedPublications(context);
        }

        private static void SeedCathedras(EntityContext context)
        {
            var cathedras = new[]
                                {
                                    new Cathedra("Кафедра тактики"),
                                    new Cathedra("Кафедра связи"),
                                    new Cathedra("Кафедра ПВО")
                                };

            foreach (var cathedra in cathedras)
            {
                context.Cathedras.Add(cathedra);
            }

            context.SaveChanges();
        }

        private static void SeedProfessors(EntityContext context)
        {
            var cathedra = context.Cathedras
                                  .AsQueryable()
                                  .Single(c => c.Name == "Кафедра связи");

            var professors = new[]
                                 {
                                     new Professor
                                     {
                                         FullName = new FullName("Сергей", "Николаевич", "Касанин"),
                                         Cathedra = cathedra,
                                         MilitaryRank = MilitaryRank.Colonel,
                                         JobPosition = JobPosition.Professor,
                                     },
                                     new Professor
                                     {
                                         FullName = new FullName("Андрей", "Васильевич", "Кашкаров"),
                                         Cathedra = cathedra,
                                         MilitaryRank = MilitaryRank.Colonel,
                                         JobPosition = JobPosition.Professor,
                                     },
                                     new Professor
                                     {
                                         FullName = new FullName("Роман", "Анатольевич", "Градусов"),
                                         Cathedra = cathedra,
                                         MilitaryRank = MilitaryRank.Colonel,
                                         JobPosition = JobPosition.Professor,
                                     },
                                     new Professor
                                     {
                                         FullName = new FullName("Дюжов", "Юрьевич", "Геннадий"),
                                         Cathedra = cathedra,
                                         MilitaryRank = MilitaryRank.Colonel,
                                         JobPosition = JobPosition.Professor,
                                     }
                                 };

            foreach (var professor in professors)
            {
                context.Professors.Add(professor);
            }

            context.SaveChanges();
        }

        private static void SeedConferences(EntityContext context)
        {
            var professor1 = context.Professors
                                    .AsQueryable()
                                    .Single(p => p.FullName.LastName == "Кашкаров");

            var professor2 = context.Set<Professor>()
                                    .AsQueryable()
                                    .Single(p => p.FullName.LastName == "Касанин");

            var conferences = new[]
                                  {
                                      new Conference
                                      {
                                          Name = "Конференция по вопросам морально-этического содержания " +
                                                 "курса молодого бойца в частях специального назначения " +
                                                 "военно-морского десанта Республики Беларусь",
                                          Date = DateTime.Parse("01.07.1993"),
                                          Curator = professor1,
                                          ConferenceType = EventLevel.University,
                                          ConferenceReport = new ConferenceReport
                                                             {
                                                                 OrganizationCorrectness = AccordanceLevel.Fully,
                                                                 ReportMaterials = AccordanceLevel.None,
                                                                 ThemeActuality = AccordanceLevel.Partly,
                                                                 ResultsUsage = AccordanceLevel.Partly
                                                             }
                                      },
                                      new Conference
                                      {
                                          Name = "Военно-научная конференция, посвященная 56-летию Великой Победы",
                                          Date = DateTime.Parse("11.09.2001"),
                                          Curator = professor1,
                                          ConferenceType = EventLevel.University,
                                          ConferenceReport = new ConferenceReport
                                                             {
                                                                 OrganizationCorrectness = AccordanceLevel.Fully,
                                                                 ReportMaterials = AccordanceLevel.None,
                                                                 ThemeActuality = AccordanceLevel.Partly,
                                                                 ResultsUsage = AccordanceLevel.Partly
                                                             }
                                      },
                                      new Conference
                                      {
                                          Name = "Вторая международная конференция",
                                          Date = DateTime.Parse("13.08.2007"),
                                          Curator = professor2,
                                          ConferenceType = EventLevel.University,
                                          ConferenceReport = new ConferenceReport
                                                             {
                                                                 OrganizationCorrectness = AccordanceLevel.Fully,
                                                                 ReportMaterials = AccordanceLevel.None,
                                                                 ThemeActuality = AccordanceLevel.Partly,
                                                                 ResultsUsage = AccordanceLevel.Partly
                                                             }
                                      }
                                  };

            foreach (var conference in conferences)
            {
                context.Conferences.Add(conference);
            }

            context.SaveChanges();
        }

        private static void SeedPublications(EntityContext context)
        {
            var professor1 = context.Professors
                                    .AsQueryable()
                                    .Single(p => p.FullName.LastName == "Кашкаров");

            var professor2 = context.Professors
                                    .AsQueryable()
                                    .Single(p => p.FullName.LastName == "Касанин");

            var books = new[]
                            {
                                new Publication
                                {
                                    Name = "Учебник по специальной подготовке",
                                    PagesCount = 228,
                                    PublicationType = PublicationType.Article,
                                    Author = professor1,
                                },
                                new Publication
                                {
                                    Name = "Учебник по тактической подготовке",
                                    PagesCount = 141,
                                    PublicationType = PublicationType.Article,
                                    Author = professor1,
                                },
                                new Publication
                                {
                                    Name = "Как стать младшим офицером, " +
                                           "начальником радиостанций коротковолновых " +
                                           "малой мощности Р142-Н, Р145-БН за 21 день " +
                                           "или курс молодого связиста для чайников",
                                    PagesCount = 1309,
                                    PublicationType = PublicationType.Article,
                                    Author = professor2,
                                }
                            };


            foreach (var book in books)
            {
                context.Books.Add(book);
            }

            context.SaveChanges();
        }
    }
}
// ReSharper restore InconsistentNaming
