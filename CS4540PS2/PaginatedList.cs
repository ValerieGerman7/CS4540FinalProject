/*
    Author:	Cody Winters
    Date:	9/14/2019
    Course:	CS 4540, University of Utah, School of Computing
    Copyright:	CS 4540 and Cody Winters - This work may not be copied for use in Academic Coursework.

    I Cody Winters, certify that I wrote this code from scratch and did not copy it in part or whole from
    any other source.Any references used in the completion of the assignemnt are cited in my README file.

    File Contents

    This file allows each of the views to be paginated, meaning that you can specify how large the table 
    for a give view (e.g. courses) should be, instead of always viewing all items.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Models;
using Microsoft.EntityFrameworkCore;

namespace CS4540PS2
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize) {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}