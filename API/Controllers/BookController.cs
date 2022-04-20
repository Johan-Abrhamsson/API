using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{

    private static List<BookJson> pList = new List<BookJson>()
    {
      new BookJson() {Name = "Book"},
      new BookJson() {Name = "Took"},
      new BookJson() {Name = "Oga"},
      new BookJson() {Name = "TookA"},
      new BookJson() {Name = "BogaAAA"},
    };

    public BookController()
    {
        BaseDocumetFix();
    }

    [HttpGet]
    public ActionResult Get()
    {
        return Ok("Initiated");
    }

    [HttpGet("name/{name}")]
    public ActionResult Get(string name)
    {
        return Ok("Response for your request for " + name);
    }



    [HttpGet]
    [Route("list")]
    public ActionResult ReadList()
    {
        return Ok(pList);
    }

    [HttpGet]
    [Route("length")]
    public ActionResult Length()
    {
        return Ok(pList.Count);
    }

    [HttpGet]
    [Route("newbook/number/{num}/{name}")]
    public ActionResult<BookJson> AddBook(int num, string name)
    {
        if (num >= 0 && num < pList.Count)
        {
            return Created($"number/{num}/{name}", $"{pList[num].Name}, and there is {pList.Count} books in total");
        }
        if (num == pList.Count)
        {
            BookJson p = new BookJson();
            p.Name = name;
            pList.Add(p);
            List<string> contents = new List<string>();
            if (System.IO.File.Exists(@"BookRemeber.txt"))
            {

                string reader = System.IO.File.ReadAllText(@"BookRemeber.txt");
                string[] names = reader.Split(' ');
                foreach (string c in names)
                {
                    contents.Add(c);
                }
                contents.Add(p.Name);
                System.IO.File.WriteAllLines(@")BookRemeber.txt", contents);
            }
            else
            {

                BaseDocumetFix();
                string reader = System.IO.File.ReadAllText(@"BookRemeber.txt");
                string[] names = reader.Split(' ');
                foreach (string c in names)
                {
                    contents.Add(c);
                }

            }
            return Created($"number/{num}/{name}", $"{p.Name}, and there is {pList.Count} books in total");
        }
        else
        {
            return NotFound();
        }
    }

    private void BaseDocumetFix()
    {
        string[] contents = new string[pList.Count];
        for (var i = 0; i < pList.Count; i++)
        {
            contents[i] = pList[i].Name;
        }

        System.IO.File.WriteAllLines(@"BookRemeber.txt", contents);

    }
}
