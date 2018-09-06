SELECT
                    e.Id,
                    e.Name,
                    e.Language,
                    s.Id,
                    s.FirstName,
                    s.LastName,
                    s.SlackHandle,
                    c.Id,
                    c.Name,
                    i.Id,
                    i.FirstName,
                    i.LastName,
                    i.SlackHandle,
                    i.Specialty,
                    se.Id,
                    se.StudentId,
                    se.ExerciseId,
                    se.InstructorId
                FROM Exercise e
                JOIN StudentExercise se ON e.Id = se.ExerciseId
                JOIN Student s ON s.Id = se.StudentId
                JOIN Cohort c ON s.CohortId = c.Id
                JOIN Instructor i ON i.Id = se.InstructorId;
				
INSERT INTO StudentExercise
                        SELECT null,
                              s.Id,
                              e.Id,
                              i.Id
                        FROM Student s, Exercise e, Instructor i WHERE s.FirstName = 'Jack Bob'
                        AND e.Name = 'ChickenMonkey'
                        AND i.FirstName = 'Jisie';