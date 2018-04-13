/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Help Desk                                    Component : Domain services                       *
*  Assembly : Empiria.KnowledgeBase.dll                    Pattern   : Data Services                         *
*  Type     : HelpDeskData                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Data read and write methods for help desk operations.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Contacts;
using Empiria.Data;

namespace Empiria.HelpDesk {

  /// <summary>Data read and write methods for help desk operations.</summary>
  static internal class HelpDeskData {


    static internal FixedList<T> GetOpenedTickets<T>(string keywords) where T : Ticket {
      string filter = GetTicketsFilter(keywords, "Status = 'P'");

      return BaseObject.GetList<T>(filter, "RequestedTime")
                       .ToFixedList();
    }

    static internal FixedList<T> SearchTickets<T>(string keywords) where T: Ticket {
      string filter = GetTicketsFilter(keywords);

      return BaseObject.GetList<T>(filter, "RequestedTime")
                       .ToFixedList();
    }


    static internal void WriteTicket(Ticket o) {
      var op = DataOperation.Parse("writeKBHelpDeskTicket",
                                    o.Id, o.TicketType.Id, o.UID,
                                    o.Customer.Id, o.Provider.Id, o.ControlNo,
                                    o.Title, o.Description, o.Tags,
                                    o.ExtensionData.ToString(), o.Keywords,
                                    o.RequestedTime, o.AssignedTo.Id,
                                    o.ResolutionTime, (char) o.Status);

      DataWriter.Execute(op);
    }


    static private string GetTicketsFilter(string keywords, string statusFilter = "") {
      string filter = String.Empty;

      if (!String.IsNullOrWhiteSpace(keywords)) {
        filter = SearchExpression.ParseAndLike("Keywords", EmpiriaString.BuildKeywords(keywords));
      }

      if (statusFilter.Length != 0) {
        filter += filter.Length != 0 ? " AND " : String.Empty;
        filter += statusFilter;
      }

      return filter;
    }

    static internal string GetNextControlNoForTicket(Contact provider) {
      var op = DataOperation.Parse("getKBLastTicketControlNoForProvider", provider.Id);

      var lastTicket = DataReader.GetScalar<string>(op, "SHL-0000");

      return EmpiriaString.IncrementCounter(lastTicket, "SHL-");
    }

  }  // class HelpDeskData

}  // namespace Empiria.HelpDesk
