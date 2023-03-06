using NUnit.Framework;
using RA;
using RestAssured;
using ClassLibrary1;
using ClassLibrary2;
using RestSharp;
using System;
using System.Text.RegularExpressions;
using ClassLibrary3;
using System.Linq;
using System.Collections.Generic;
using ClassLibrary5;
using Newtonsoft.Json;

namespace TwoApiTest
{
    [TestFixture]

    public class Tests : Specification
    {
        /*
         * ��������� ������ http://reqres.in/, �������� ������ ������������� �� ������ ��������;
         * ���������, ��� ����� ������-�������� ������������� ���������;
         * ���������, ��� email ������������� ����� ��������� reqres.in;
         */

        [TestCase]
        public void Test1()
        {
            var library1 = new Class1();
            var response = GetMethod("/api/users?page=2");
            var users = library1.GetUsers(response);

            Assert.AreEqual(StatusCode200(), GetStatusCode(response), "Error statusCode");

            for (int i = 0; i < users.data.Count; i++)
            {
                // ��������, � �������������� ������������ ���������
                Assert.AreEqual(Convert.ToInt32(Regex.Replace(users.data[i].avatar, @"[^\d]+", "")), users.data[i].id, "Error");
                // �������� �� ������� id � email � ������ ������ Contains
                Assert.IsTrue(users.data[i].avatar.Contains(users.data[i].id.ToString()));
            }

            for (int i = 0; i < users.data.Count; i++)
            {
                Assert.IsTrue(users.data[i].email.EndsWith("reqres.in"));
            }
        }

        /*
         * ��������� ������ http://reqres.in/, �������������� ����������� ����������� � �������;
         * ���������� �������� 2 ������:
         * - �������� �����������;
         * - ����������� � �������� ��-�� ���������� ������;
         * - ��������� ���� ������;
         */

        [TestCase]
        public void Test2()
        {
            var library2 = new Class2();
            Registration reg = new Registration("eve.holt@reqres.in", "pistol");
            string json = JsonConvert.SerializeObject(reg);
            var response = PostMethod("api/register", json);
            var resultRegistration = library2.SuccessfulResultRegistration(response);

            Assert.AreEqual(StatusCode200(), GetStatusCode(response), "Error statusCode");

            Assert.NotNull(resultRegistration.id, "NULL");
            Assert.NotNull(resultRegistration.token, "NULL");

            Assert.AreEqual(library2.id, resultRegistration.id, "Error registration");
            Assert.AreEqual(library2.token, resultRegistration.token, "Error registration");
        }

        [TestCase]
        public void Test3()
        {
            var library2 = new Class2();
            Registration reg = new Registration("sydney@fife", "");
            string json = JsonConvert.SerializeObject(reg);
            var response = PostMethod("api/register", json);
            var resultRegistration = library2.UnSuccessfulRegistration(response);

            Assert.AreEqual(StatusCode400(), GetStatusCode(response), "Error statusCode");
            Assert.AreEqual("Missing password", resultRegistration.Error, "Error");
        }

        ///*
        // * ��������� ������ http://reqres.in/, ���������, ��� �������� LIST<RESOURCE> ���������� ������, ��������������� �� �����;
        // */

        [TestCase]
        public void Test4()
        {
            var library3 = new Class3();
            var response = GetMethod("api/unknown");
            var years = library3.Years(response);
            var expectedSortDate = years.OrderBy(x => x);

            Assert.AreEqual(StatusCode200(), GetStatusCode(response), "Error statusCode");
            Assert.IsTrue(expectedSortDate.SequenceEqual(years), "The sort years is wrong");
        }

        ///*
        //* ��������� ������ http://reqres.in/, ����������� ������� ������� ������������ � �������� ������-���;
        //*/

        [TestCase]
        public void Test5()
        {
            var response = DeleteMethod("/api/users/2");

            Assert.AreEqual(StatusCode204(), GetStatusCode(response), "Error statusCode");
        }

        ///*
        //* ��������� ������ http://reqres.in/, �������� ���������� � ������������ � �������� ���� ���������� � ������� ����� �� ������;
        //*/

        [TestCase]
        public void Test6()
        {
            var library5 = new Class5();
            UpdateDate updateDate = new UpdateDate("morpheus", "zion resident");
            string json = JsonConvert.SerializeObject(updateDate);
            var response = PutMethod("/api/users/2", json);
            Assert.AreEqual(StatusCode200(), GetStatusCode(response), "Error statusCode");

            string dateUpdate = library5.Put(response);
            DateTimeOffset dateToday = DateTime.Today;
            string s = Regex.Replace(dateToday.ToString(), @"\s\d:\d{2}:\d{2}\s\+\d{2}:\d{2}", "");

            Assert.AreEqual(s, dateUpdate, "Error");
        }
    }
}