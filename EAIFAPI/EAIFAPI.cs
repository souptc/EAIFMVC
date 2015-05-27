using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using EAIFMVC.Models;
using EAIFMVC.EAIFDataServiceReference;

namespace EAIFMVC.EAIFAPI
{
    public class EAIFAPI
    {
        public static T1 ConversionBetweenEnums<T1, T2>(T2 v2)
            where T1 : struct
            where T2 : struct
        {
            return (T1)Enum.Parse(typeof(T1), v2.ToString(), true);
        }

        public static string GetDescription(Models.AlertStatus value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        private static readonly JavaScriptSerializer _serializer = CreateSerializer();

        private static JavaScriptSerializer CreateSerializer()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer() { MaxJsonLength = 86753090 };
            return serializer;
        }

        public static string JsonEncode(object value)
        {
            // Serialize our dynamic array type as an array
            DynamicJsonArray jsonArray = value as DynamicJsonArray;
            if (jsonArray != null)
            {
                return _serializer.Serialize((object[])jsonArray);
            }

            return _serializer.Serialize(value);

        }

        public static CompanyWrapper ConvertFromDBCompany(Company company)
        {
            var result = new CompanyWrapper();
            result.ID = company.ID;
            result.Name = company.Name;
            result.LawPersion = company.LawPerson;
            result.Category =
                ConversionBetweenEnums<EAIFMVC.Models.CompanyCategory, EAIFMVC.EAIFDataServiceReference.CompanyCategory>(
                    company.Category);
            result.ContactorID = company.ContactorID;
            result.ContactorName = company.contactorName;
            result.Phone = company.Phone;
            result.Size = company.Size;
            result.Storage = company.Storage;
            result.DangerCategory =
                ConversionBetweenEnums<EAIFMVC.Models.DangerCategory, EAIFMVC.EAIFDataServiceReference.DangerCategory>(
                    company.DangerCategory);
            return result;
        }

        public static AlertWrapper ConvertFromDBAlert(Alert alert)
        {
            var result = new AlertWrapper();
            result.ID = alert.ID;
            result.CompanyID = alert.CompanyID;
            result.AlertTime = alert.AlertTime;
            result.Status =
                ConversionBetweenEnums<Models.AlertStatus, EAIFDataServiceReference.AlertStatus>(alert.Status);
            result.DangerSource = alert.DangerSource;
            result.StatusDescription = GetDescription(result.Status);
            return result;
        }

        public static ContactorWrapper ConvertFromDBContactor(Contactor contactor)
        {
            var result = new ContactorWrapper();
            result.Id = contactor.Id;
            result.Name = contactor.Name;
            result.Phone = contactor.phone;
            result.CompanyName = contactor.CompanyName;
            return result;
        }

        public static NotificationWrapper ConvertFromDBNotificationWrapper(Notification notification)
        {
            var result = new NotificationWrapper();
            result.Id = notification.Id;
            result.Status =
                ConversionBetweenEnums<Models.NotficationStatus, EAIFDataServiceReference.NotficationStatus>(
                    notification.Status);
            result.Text = notification.Text;
            result.Title = notification.Title;
            result.Type =
                ConversionBetweenEnums<Models.NotificationType, EAIFDataServiceReference.NotificationType>(
                    notification.Type);
            return result;
        }

        public static CompanyWrapper LoadCompanyWrapperById(Guid id, out string errorMessage)
        {
            try
            {
                errorMessage = "";
                using (var client = new EAIFDataServiceClient())
                {
                    var company = client.LoadCompany(id, out errorMessage);
                    if (company != null)
                        return ConvertFromDBCompany(company);
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return null;
            }
        }

        public static List<CompanyWrapper> LoadCompanyList(out string errorMessage)
        {
            try
            {
                errorMessage = "";
                using (var client = new EAIFDataServiceClient())
                {
                    var companyList = client.LoadCompanyList(out errorMessage);
                    if (companyList != null)
                    {
                        var result = new List<CompanyWrapper>();
                        foreach (var company in companyList)
                        {
                            result.Add(ConvertFromDBCompany(company));
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return null;
            }
        }

        public static AlertWrapper LoadAlertWrapper(Guid id, out string errorMessage)
        {
            try
            {
                errorMessage = "";
                using (var client = new EAIFDataServiceClient())
                {
                    var alert = client.LoadAlert(id, out errorMessage);
                    if (alert != null)
                        return ConvertFromDBAlert(alert);
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return null;
            }
        }

        public static List<AlertWrapper> LoadAlertList(out string errorMessage)
        {
            try
            {
                errorMessage = "";
                using (var client = new EAIFDataServiceClient())
                {
                    var alertList = client.LoadAlertList(out errorMessage);
                    if (alertList != null)
                    {
                        var result = new List<AlertWrapper>();
                        foreach (var alert in alertList)
                        {
                            result.Add(ConvertFromDBAlert(alert));
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return null;
            }
        }

        public static List<AlertWrapper> SearchAlerts(DateTime? start, DateTime? end, out string errorMessage)
        {
            try
            {
                errorMessage = "";
                using (var client = new EAIFDataServiceClient())
                {
                    var alertList = client.SearchAlerts(start, end, out errorMessage);
                    if (alertList != null)
                    {
                        var result = new List<AlertWrapper>();
                        foreach (var alert in alertList)
                        {
                            result.Add(ConvertFromDBAlert(alert));
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return null;
            }
        }

        public static List<ContactorWrapper> LoadContactorList(out string errorMessage)
        {
            try
            {
                errorMessage = "";
                using (var client = new EAIFDataServiceClient())
                {
                    var contactorList = client.LoadContactorList(out errorMessage);
                    if (contactorList != null)
                    {
                        var result = new List<ContactorWrapper>();
                        foreach (var contactor in contactorList)
                        {
                            result.Add(ConvertFromDBContactor(contactor));
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return null;
            }
        }

        public static List<NotificationWrapper> LoadNotificationList(out string errorMessage)
        {
            try
            {
                errorMessage = "";
                using (var client = new EAIFDataServiceClient())
                {
                    var notificationList = client.LoadNotificationList(out errorMessage);
                    if (notificationList != null)
                    {
                        var result = new List<NotificationWrapper>();
                        foreach (var notification in notificationList)
                        {
                            result.Add(ConvertFromDBNotificationWrapper(notification));
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return null;
            }
        }
    }
}