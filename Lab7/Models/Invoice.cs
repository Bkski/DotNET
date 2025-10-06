using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab7.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int CustomerId { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Invoice Date")]
    public DateTime InvoiceDate { get; set; }

    [Display(Name = "Billing Address")]
    public string? BillingAddress { get; set; }

    [Display(Name = "City")]
    public string? BillingCity { get; set; }

    [Display(Name = "State")]
    public string? BillingState { get; set; }

    [Display(Name = "Country")]
    public string? BillingCountry { get; set; }

    public string? BillingPostalCode { get; set; }


    [DataType(DataType.Currency)]
    [Display(Name = "Total Amount")]
    public decimal Total { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
}
