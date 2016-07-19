using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using University.Objects;

namespace University
{
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=university_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
       Student.DeleteAll();
       Course.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange & Act
      int result = Student.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange & Act
      Student firstStudent = new Student("Sam", "February 5th, 2016");
      Student secondStudent = new Student("Sam", "February 5th, 2016");

      //Assert
      Assert.Equal(firstStudent, secondStudent);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Student testStudent = new Student("Sam", "February 5th, 2016");

      //Act
      testStudent.Save();
      List<Student> result = Student.GetAll();
      List<Student> testList = new List<Student>{testStudent};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Student testStudent = new Student("Sam", "February 5th, 2016");

      //Act
      testStudent.Save();
      Student savedStudent = Student.GetAll()[0];

      int result = savedStudent.GetId();
      int testId = testStudent.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsStudentInDatabase()
    {
      //Arrange
      Student testStudent = new Student("Sam", "February 5th, 2016");
      testStudent.Save();

      //Act
      Student foundStudent = Student.Find(testStudent.GetId());

      //Assert
      Assert.Equal(testStudent, foundStudent);
    }

    [Fact]
    public void Test_Delete_DeletesStudentFromDatabase()
    {
      //Arrange
      Student firstStudent = new Student("Sam", "February 5th, 2016");
      firstStudent.Save();
      Student secondStudent = new Student("Sam", "February 5th, 2016");
      secondStudent.Save();

      //Act
      firstStudent.Delete();
      List<Student> result = Student.GetAll();
      List<Student> testResult = new List<Student>{secondStudent};

      //Assert
      Assert.Equal(result, testResult);
    }


  }
}
