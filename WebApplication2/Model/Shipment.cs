
using System;
using System.Collections.Generic;

namespace WebApplication2.Model
{
    public class _PagingInfo
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int ItemsOnPage { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class ShipmentResponse
    {
        public _PagingInfo _PagingInfo { get; set; }
        public List<Shipment> Items { get; set; }
    }

    public class Shipment
    {
        public Links Links { get; set; }
        public long Id { get; set; }
        public long ModifiedTimeStamp { get; set; }
        public string ShipFromPartyName { get; set; }
        public Location ShipFromLocation { get; set; }
        public string ShipFromLocationStatus { get; set; }
        public DateTime EstimatedDepartureDate { get; set; }
        public DateTime ActualDepartureDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string ShipToPartyName { get; set; }
        public Location ShipToLocation { get; set; }
        public string ShipToLocationStatus { get; set; }
        public DateTime EstimatedArrivalDate { get; set; }
        public DateTime ActualArrivalDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string SoldBy { get; set; }
        public string SoldTo { get; set; }
        public Event LastEvent { get; set; }
        public string Status { get; set; }
        public List<References> References { get; set; }
        public Equipment Equipment { get; set; }
        public int Progress { get; set; }
        public string OnTimeStatus { get; set; }
        public Emission Emission { get; set; }
        public DateTime ReadyDate { get; set; }
        public string MainModality { get; set; }
    }

    public class Links
    {
        public string Href { get; set; }
        public string Rel { get; set; }
    }

    public class Location
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CountrySubEntityName { get; set; }
        public string CountrySubEntityCode { get; set; }
    }

    public class Event
    {
        public Links Links { get; set; }
        public long Id { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public long LastUpdate { get; set; }
        public string Text { get; set; }
        public string Template { get; set; }
        public List<TemplateVariable> TemplateVariables { get; set; }
    }

    public class TemplateVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
        public string DataTypeFormat { get; set; }
        public Resource Resource { get; set; }
        public bool Primary { get; set; }
    }

    public class Resource
    {
        public Links Links { get; set; }
        public long Id { get; set; }
        public string Type { get; set; }
    }

    public class References
    {
        public string Reference{ get; set; }
        public string Role { get; set; }
    }

    public class Equipment
    {
        public string Number { get; set; }
        public string Classification { get; set; }
        public string Type { get; set; }
        public List<object>? TransportReferences { get; set; }
        
        public List<string> SealNumbers { get; set; }
    }

    public class TransportReferences
    {
        public string? Description { get; set; }
        public int? Number { get; set; }
    }

    public class Emission
    {
        public string Calculation { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
    }
}