﻿using FluentValidation;

namespace Core.Models;
public class MasjeedModel
{
    public string FId { get; set; }
    public Guid Id { get; set; }
    public string MasjeedName { get; set; }
    public string KeyInfluencer { get; set; }
    public bool DoesImamSupportsVaccine { get; set; }
    public bool DoYouHavePermissionForAdsInMasjeed { get; set; }

    public MasjeedModel()
    {
        Id = Guid.NewGuid();
    }
    public bool IsEmpty()
    {
        if (Id == Guid.Empty && MasjeedName is null && KeyInfluencer is null)
        {
            return true;
        }
        return false;
    }
}
public class MasjeedValidator : AbstractValidator<MasjeedModel>
{
    public MasjeedValidator()
    {
        RuleFor(m => m.MasjeedName)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
            .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");

    }
    protected bool BeAValidName(string name)
    {
        name = name.Replace(" ", "");
        name = name.Replace("-", "");
        return name.All(Char.IsLetter);
    }
}