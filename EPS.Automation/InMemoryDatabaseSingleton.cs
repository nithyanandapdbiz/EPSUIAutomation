using EPS.Automation.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace EPS.Automation
{
    /// <summary>
    /// Thi class is a singleton.
    /// </summary>
    internal class InMemoryDatabaseSingleton
    {
        /// <summary>
        /// This is the in memory database.
        /// </summary>
        private readonly InMemoryDatabase _inMemoryDatabase = null;

        /// <summary>
        /// This is the singleton instance class.
        /// </summary>
        private static InMemoryDatabaseSingleton _inMemoryDatabaseSingleton = null;

        /// <summary>
        /// This is the private constructor of the database and
        /// deserialize xml data in memory based on environment variable.
        /// </summary>
        private InMemoryDatabaseSingleton()
        {
            _inMemoryDatabase = new InMemoryDatabase();
            this.DeserializeTheXmlDataInMemory(GetInMemoryTestDataFilePath());
        }

        /// <summary>
        /// Deserialize The XML Data In Memory.
        /// </summary>
        /// <param name="xmlTestDataFilePath">This is the xml file path.</param>
        private void DeserializeTheXmlDataInMemory(String xmlTestDataFilePath)
        {
            // get xml data based on file path
            String getXmlData = File.ReadAllText(xmlTestDataFilePath);
            var xmlDocument = new XmlDocument();
            // load xml data
            xmlDocument.LoadXml(getXmlData);
            // created xml serializer
            XmlNodeList xmlNodeList;
            XmlSerializer xmlSerializer;
            // desearlize data
            DesearlizeUserTestData(xmlDocument, out xmlNodeList, out xmlSerializer);
        }

        /// <summary>
        /// Get the In Memory Test Data XML File Path.
        /// </summary>
        /// <param name="applicationEnvironment">This is application nvironment name.</param>
        /// <returns>In memory TestData xml file path.</returns>
        private static String GetInMemoryTestDataFilePath()
        {
            //copy xml file to TestResults/TestData folder for first time
            //and return the xml file path
            string srcXmlFilePath = Path.Combine(new string[] {
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
               AutomationConfigurationManagerResource.InMemoryTestData_Path,AutomationConfigurationManagerResource.XMLFolderName,
               AutomationConfigurationManagerResource.TestData_Path + ".xml" });
            string destinationXMLPath = Path.Combine(new string[] {
                Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).
                GetDirectories("TestResults")[0].FullName,
                AutomationConfigurationManagerResource.TestData_Path,
                AutomationConfigurationManagerResource.TestData_Path + ".xml"});

            string destinationFolderPath = Path.Combine(new string[] {
             Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).
             GetDirectories("TestResults")[0].FullName,
            AutomationConfigurationManagerResource.TestData_Path });
            if (!Directory.Exists(destinationFolderPath))
            {
                Directory.CreateDirectory(destinationFolderPath);
            }
            if (!File.Exists(destinationXMLPath))
            {
                File.Copy(srcXmlFilePath, destinationXMLPath, false);
            }
            return destinationXMLPath;
        }

        /// <summary>
        /// Desearlize User Test Data In Memory.
        /// </summary>
        /// <param name="xmlDocument">Represents an XML document.</param>
        /// <param name="xmlNodeList">Represents an ordered collection of nodes.</param>
        /// <param name="xmlSerializer">Serializes and deserializes objects into and from XML documents.
        /// The XmlSerializer enables you to control how objects are encoded into XML.</param>
        private void DesearlizeUserTestData(XmlDocument xmlDocument,
            out XmlNodeList xmlNodeList, out XmlSerializer xmlSerializer)
        {
            // get xml node list for courses
            xmlNodeList = xmlDocument.SelectNodes("Data/ArrayOfUser");
            // created object xml serializer
            xmlSerializer = new XmlSerializer(typeof(List<User>));
            if (xmlNodeList != null && xmlNodeList.Count > 0)
            {
                // created object xml node reader
                var reader = new XmlNodeReader(xmlNodeList.Item(0));
                // get users list
                var getUserList = (List<User>)
                 xmlSerializer.Deserialize(reader);
                foreach (User users in getUserList)
                {
                    // push in memory
                    _inMemoryDatabase.Insert(users);
                }
            }
        }

        /// <summary>
        /// This class returns the instance of the in memory database
        /// </summary>
        /// <returns></returns>
        private static InMemoryDatabaseSingleton GetInstance()
        {
            //if the instance doesnt exist then create a new one
            return _inMemoryDatabaseSingleton ?? (_inMemoryDatabaseSingleton = new InMemoryDatabaseSingleton());
        }

        /// <summary>
        /// This is the instance of the database
        /// </summary>
        public static InMemoryDatabase DatabaseInstance
        {
            get { return GetInstance()._inMemoryDatabase; }
        }

        /// <summary>
        /// Save user dynamic test data in memory
        /// </summary>
        /// <param name="user">This is user type enum.</param>
        /// <param name="entityValue">This is entity type.</param>
        public static void SaveUserTestData(User user, string entityType, string entityValue)
        {
            string filePath = GetInMemoryTestDataFilePath();
            string getXmlData = File.ReadAllText(filePath);
            var xmlDocument = new XmlDocument();
            //// load xml data
            xmlDocument.LoadXml(getXmlData);
            var elements = xmlDocument.GetElementsByTagName("User");
            foreach (XmlNode eletment in elements)
            {
                // Update the data only of the user type matches
                if (eletment.ChildNodes[2].InnerText == user.UserType.ToString())
                {
                    // Update the user data based on the entity
                    switch (entityType)
                    {
                        case "Name":
                            eletment.ChildNodes[0].InnerText = entityValue;
                            break;

                        case "Password":
                            eletment.ChildNodes[1].InnerText = entityValue;
                            break;
                    }
                    break;
                }
            }
            xmlDocument.Save(filePath);
        }
    }
}