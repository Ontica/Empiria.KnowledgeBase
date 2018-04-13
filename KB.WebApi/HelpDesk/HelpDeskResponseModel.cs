/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Help Desk                                    Component : Web Api                               *
*  Assembly : Empiria.KnowledgeBase.WebApi.dll             Pattern   : Response methods                      *
*  Type     : HelpDeskResponseModel                        License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Response static methods for help desk tickets and their related entities.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;

using Empiria.Contacts;

using Empiria.KnowledgeBase;
using Empiria.KnowledgeBase.WebApi;

namespace Empiria.HelpDesk.WebApi {

  /// <summary>Response static methods for help desk tickets and their related entities.</summary>
  static internal class HelpDeskResponseModel {

    static internal ICollection ToResponse(this IList<Ticket> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var ticket in list) {
        var item = ticket.ToResponse();

        array.Add(item);
      }
      return array;
    }


    static internal object ToResponse(this Ticket ticket) {
      if (ticket is MeetingReport) {
        return ToResponse((MeetingReport) ticket);
      }

      throw Assertion.AssertNoReachThisCode();
    }

    static internal object ToResponse(this MeetingReport meeting) {
      return new {
        uid = meeting.UID,
        type = meeting.TicketType.Name,
        typeName = meeting.TicketType.DisplayName,
        controlNo = meeting.ControlNo,
        title = meeting.Title,
        description = meeting.Description,
        requestedTime = meeting.RequestedTime,
        assignedTo = meeting.AssignedTo.Alias,
        customerName = meeting.Customer.Alias,
        location = meeting.Location,
        date = meeting.Date,
        startTime = meeting.StartTime,
        endTime = meeting.EndTime,
        resolutionTime = meeting.ResolutionTime,
        status = meeting.Status,

        participants = meeting.Participants.ToResponse(),
        topics = new[] { new { uid = "sdkfgb345", name = "ASEA" }, new { uid = "nm23511", name = "Pozo XASD-3" } },
        recommendations = new Faq[2] { Faq.Parse("jkldfvg3JJ452"), Faq.Parse("jkldfvg3JJ452") }.ToResponse(),
        agreements = new string[0]
      };

    }

    static internal ICollection ToResponse(this IList<Contact> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var contact in list) {
        var item = new {
          uid = contact.UID,
          shortName = contact.Nickname,
        };
        array.Add(item);
      }

      return array;
    }

  }  // class HelpDeskResponseModel

}  // namespace Empiria.HelpDesk.WebApi
