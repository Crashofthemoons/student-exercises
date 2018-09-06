using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Collections;
using Dapper;

namespace nss.Data
{
    public class DatabaseInterface
    {
        public static SqliteConnection Connection
        {
            get
            {
                /*
                    Mac users: You can create an environment variable in your
                    .zshrc file.
                        export NSS_DB="/path/to/your/project/nss.db"

                    Windows users: You need to use a property window
                        http://www.forbeslindesay.co.uk/post/42833119552/permanently-set-environment-variables-on-windows
                 */
                string env = $"{Environment.GetEnvironmentVariable("NSS_DB")}";
                string _connectionString = $"Data Source={env}";
                return new SqliteConnection(_connectionString);
            }
        }


        public static void CheckCohortTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Cohort> cohorts = db.Query<Cohort>
                    ("SELECT Id FROM Cohort").ToList();
                    Console.WriteLine("cohort table select");
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                Console.WriteLine("create cohort table");
                    db.Execute(@"CREATE TABLE Cohort (
                        `Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `Name`	TEXT NOT NULL UNIQUE
                    )");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Evening Cohort 1')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 10')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 11')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 12')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 13')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 21')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 26')");

                }
            }
        }

        public static void CheckInstructorsTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Instructor> toys = db.Query<Instructor>
                    ("SELECT Id FROM Instructor").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute(@"CREATE TABLE Instructor (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `FirstName`	varchar(80) NOT NULL,
                        `LastName`	varchar(80) NOT NULL,
                        `SlackHandle`	varchar(80) NOT NULL,
                        `Specialty` varchar(80),
                        `CohortId`	integer NOT NULL,
                        FOREIGN KEY(`CohortId`) REFERENCES `Cohort`(`Id`)
                    )");

                    db.Execute(@"INSERT INTO Instructor
                        SELECT null,
                              'Steve',
                              'Brownlee',
                              '@coach',
                              'Dad jokes',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Evening Cohort 1'
                    ");

                    db.Execute(@"INSERT INTO Instructor
                        SELECT null,
                              'Joe',
                              'Shepherd',
                              '@joes',
                              'Analogies',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 13'
                    ");

                    db.Execute(@"INSERT INTO Instructor
                        SELECT null,
                              'Jisie',
                              'David',
                              '@jisie',
                              'Student success',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 21'
                    ");
                }
            }
        }


        public static void CheckExercisesTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Exercise> exercises = db.Query<Exercise>
                    ("SELECT Id FROM Exercise").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute(@"CREATE TABLE Exercise (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `Name`	varchar(80) NOT NULL,
                        `Language`	varchar(80) NOT NULL
                    )");

                    db.Execute(@"INSERT INTO Exercise
                        SELECT null,
                              'ChickenMonkey',
                              'JavaScript'
                    ");

                    db.Execute(@"INSERT INTO Exercise
                        SELECT null,
                              'Nickelback',
                              'C#'
                    ");

                    db.Execute(@"INSERT INTO Exercise
                        SELECT null,
                              'Poopinabucket',
                              'BrownScript'
                    ");
                }
            }
        }

        public static void CheckStudentsTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Student> students = db.Query<Student>
                    ("SELECT Id FROM Student").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute(@"CREATE TABLE Student (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `FirstName`	varchar(80) NOT NULL,
                        `LastName`	varchar(80) NOT NULL,
                        `SlackHandle`	varchar(80) NOT NULL,
                        `CohortId`	integer NOT NULL,
                        FOREIGN KEY(`CohortId`) REFERENCES `Cohort`(`Id`)
                    )");

                    db.Execute(@"INSERT INTO Student
                        SELECT null,
                              'Brett',
                              'Shearin',
                              '@brettshearin',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 26'
                    ");

                    db.Execute(@"INSERT INTO Student
                        SELECT null,
                              'Jenn',
                              'Lawson',
                              '@crashofthemoons',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 26'
                    ");

                    db.Execute(@"INSERT INTO Student
                        SELECT null,
                              'Jack Bob',
                              'Henderson',
                              '@jacobhenderson',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 26'
                    ");
                }
            }
        }

        public static void CheckStudentExercisesTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<StudentExercise> studentExercises = db.Query<StudentExercise>
                    ("SELECT Id FROM StudentExercise").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute(@"CREATE TABLE StudentExercise (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `StudentId`	integer NOT NULL,
                        `ExerciseId`	integer NOT NULL,
                        `InstructorId`	integer NOT NULL,
                        FOREIGN KEY(`StudentId`) REFERENCES `Student`(`Id`),
                        FOREIGN KEY(`ExerciseId`) REFERENCES `Exercise`(`Id`),
                        FOREIGN KEY(`InstructorId`) REFERENCES `Instructor`(`Id`)
                    )");

                    db.Execute(@"INSERT INTO StudentExercise
                        SELECT null,
                              s.Id,
                              e.Id,
                              i.Id
                        FROM Student s, Exercise e, Instructor i WHERE s.FirstName = 'Brett'
                        AND e.Name = 'ChickenMonkey'
                        AND i.FirstName = 'Jisie'
                    ");

                    db.Execute(@"INSERT INTO StudentExercise
                        SELECT null,
                              s.Id,
                              e.Id,
                              i.Id
                        FROM Student s, Exercise e, Instructor i WHERE s.FirstName = 'Jack Bob'
                        AND e.Name = 'Nickelback'
                        AND i.FirstName = 'Steve'
                    ");

                    db.Execute(@"INSERT INTO StudentExercise
                        SELECT null,
                              s.Id,
                              e.Id,
                              i.Id
                        FROM Student s, Exercise e, Instructor i WHERE s.FirstName = 'Jenn'
                        AND e.Name = 'Poopinabucket'
                        AND i.FirstName = 'Joe'
                    ");
                }
            }
        }
    }
}