﻿using Core.Features;
using Core.Models;
using DAL.Persistence;
using RealCache.Persistence.Migrations;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status.Vaccine;

public class AddVaccineViewModel : ViewModelBase
{
    public string _childId;
    private VaccineModel _vaccine;
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private readonly DbContext<PeriodModel> _dbContext;

    public AddVaccineViewModel(UnitOfWork unitOfWork, IToast toast, DbContext<PeriodModel> dbContext)
    {
        Vaccine = new();
        PostCommand = new Command(Post);
        _unitOfWork = unitOfWork;
        _toast = toast;
        _dbContext = dbContext;
    }

    private async void Post()
    {
        _dbContext.CreateDB("mobilizer", "user");
        var s = _dbContext.Get();

        try
        {
            Vaccine.Date = DateTime.SpecifyKind(Vaccine.Date, DateTimeKind.Utc);
            Vaccine.Period = s.First().Id.ToString();
            await _unitOfWork.AddVaccine(Vaccine, _childId);
            await Shell.Current.GoToAsync("..");
            _toast.MakeToast("Vaccine Added");
        }
        catch (Exception)
        {
            return;
        }
    }

    public void GetQueryProperty(string childId)
    {
        _childId = childId;
    }

    public VaccineModel Vaccine
    {
        get { return _vaccine; }
        set { _vaccine = value; OnPropertyChanged(); }
    }

    public ICommand PostCommand { private set; get; }
}
