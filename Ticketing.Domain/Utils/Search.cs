using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Domain.Models;
using Ticketing.Domain.Data;


namespace Ticketing.Domain.Utils;

public static class SearchUtils
{
    // Recursivo: aplica un predicado y llama con el resto
    public static List<Event> RecursiveFilter(List<Event> source, List<Func<Event, bool>> predicates, int idx = 0)
    {
        if (predicates is null || predicates.Count == 0) return source;
        if (idx >= predicates.Count) return source;

        var filtered = source.Where(predicates[idx]).ToList();
        return RecursiveFilter(filtered, predicates, idx + 1);
    }

    // Iterativo: aplica los predicados en un bucle
    public static List<Event> IterativeFilter(List<Event> source, List<Func<Event, bool>> predicates)
    {
        if (predicates is null || predicates.Count == 0) return source;
        foreach (var p in predicates)
            source = source.Where(p).ToList();
        return source;
    }
}
