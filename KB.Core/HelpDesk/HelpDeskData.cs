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

using Empiria.Data;

namespace Empiria.HelpDesk {

  /// <summary>Data read and write methods for help desk operations.</summary>
  static internal class HelpDeskData {


    static internal FixedList<Ticket> GetOpenedTickets(string keywords) {
      string filter = GetTicketsFilter(keywords, "Status = 'P'");

      return BaseObject.GetList<Ticket>(filter, "RequestedTime")
                       .ToFixedList();
    }

    static internal FixedList<Ticket> SearchTickets(string keywords) {
      string filter = GetTicketsFilter(keywords);

      return BaseObject.GetList<Ticket>(filter, "RequestedTime")
                       .ToFixedList();
    }


    static internal void WriteTicket(Ticket o) {
      var op = DataOperation.Parse("writeServiceDeskTicket",
                                    o.Id, o.GetEmpiriaType().Id, o.UID,
                                    o.Customer.Id, o.Provider.Id, o.ControlNo,
                                    o.Title, o.Description, o.Tags,
                                    o.ExtensionData.ToString(), o.Keywords,
                                    o.RequestedTime, o.ResolutionTime,
                                    (char) o.Status);

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

  }  // class HelpDeskData

}  // namespace Empiria.HelpDesk
