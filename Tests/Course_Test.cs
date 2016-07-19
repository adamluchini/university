using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using University.Objects;

namespace University
{
  public class CourseTest : IDisposable
  {
    public CourseTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=university_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
       Course.DeleteAll();
       Student.DeleteAll();
    }

    [Fact]
    public void Test_CourseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Course.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Course firstCourse = new Course("Intro to Programming", "PR101");
      Course secondCourse = new Course("Intro to Programming", "PR101");

      //Assert
      Assert.Equal(firstCourse, secondCourse);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Course testCourse = new Course("Intro to Programming", "PR101");
      testCourse.Save();

      //Act
      List<Course> result = Course.GetAll();
      List<Course> testList = new List<Course>{testCourse};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Course testCourse = new Course("Intro to Programming", "PR101");
      testCourse.Save();

      //Act
      Course savedCourse = Course.GetAll()[0];

      int result = savedCourse.GetId();
      int testId = testCourse.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsCourseInDatabase()
    {
      //Arrange
      Course testCourse = new Course("Intro to Programming", "PR101");
      testCourse.Save();

      //Act
      Course foundCourse = Course.Find(testCourse.GetId());

      //Assert
      Assert.Equal(testCourse, foundCourse);
    }


    [Fact]
    public void Test_GetStudents_RetrievesAllStudentsWithCategory()
    {
      Course testCourse = new Course("Intro to Programming", "PR101");
      testCourse.Save();

      Student firstStudent = new Student("Sam", "February 5th, 2016");
      firstStudent.Save();
      Student secondStudent = new Student("Sam", "February 5th, 2016");
      secondStudent.Save();

      testCourse.AddStudent(firstStudent);
      List<Student> testStudentList = new List<Student> {firstStudent};
      List<Student> resultStudentList = testCourse.GetStudents();

      Assert.Equal(testStudentList, resultStudentList);
    }


    [Fact]
    public void Test_Delete_DeletesCourseFromDatabase()
    {
      //Arrange
      Course firstCourse = new Course("Intro to Programming", "PR101");
      firstCourse.Save();

      Course secondCourse = new Course("Javascript", "JS200");
      secondCourse.Save();

      //Act
      firstCourse.Delete();
      List<Course> resultCourses = Course.GetAll();
      List<Course> testCourseList = new List<Course> {secondCourse};

      //Assert
      Assert.Equal(testCourseList, resultCourses);
    }

  }
}
