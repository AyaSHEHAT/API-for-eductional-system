﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIDay2.Models;

[Keyless]
[Table("DailyTransaction")]
public partial class DailyTransaction
{
    public int? usrId { get; set; }

    public int? DtransAmount { get; set; }
}