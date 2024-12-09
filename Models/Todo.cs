using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;

public class Todo
{
    public int Id { get; set;}

    [Required(ErrorMessage ="Tittle er påkrevd.")] //gjør tittel påkrevd
    [StringLength(100, MinimumLength= 3, ErrorMessage ="Tittel kan ikke være lenger enn 100 tegn.")]
    public string? Title {get; set; }

    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Bekrivelse må bestå av minimum 10 tegn")]
    public string? Discription {get; set;}

    public bool IsCompleted {get; set;} = false; 


    public int CategoryId {get; set;}   //fremmednøkkel
    public Category? Category {get; set;} //navigasjons-egenskap 
}