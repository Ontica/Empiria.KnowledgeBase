/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Help Desk                                    Component : Domain services                       *
*  Assembly : Empiria.KnowledgeBase.dll                    Pattern   : Power type                            *
*  Type     : TicketType                                   License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Power type used to describe a help desk ticket.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Ontology;

namespace Empiria.HelpDesk {

  /// <summary>Power type used to describe a help desk ticket.</summary>
  [Powertype(typeof(Ticket))]
  public class TicketType : Powertype {

    #region Constructors and parsers

    private TicketType() {
      // Empiria power types always have this constructor.
    }

    static public new TicketType Parse(int typeId) {
      return ObjectTypeInfo.Parse<TicketType>(typeId);
    }


    static internal new TicketType Parse(string typeName) {
      return ObjectTypeInfo.Parse<TicketType>(typeName);
    }


    static public TicketType CustomerCall {
      get {
        return TicketType.Parse("ObjectType.ServiceDeskTicket.CustomerCall");
      }
    }


    static public TicketType MeetingReport {
      get {
        return TicketType.Parse("ObjectType.ServiceDeskTicket.MeetingReport");
      }
    }


    #endregion Constructors and parsers

  } // class TicketType

} // namespace Empiria.HelpDesk
