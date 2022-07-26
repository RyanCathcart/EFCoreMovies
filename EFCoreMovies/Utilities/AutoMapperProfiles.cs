﻿using AutoMapper;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;

namespace EFCoreMovies.Utilities;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Actor, ActorDto>();

        CreateMap<Cinema, CinemaDto>()
            .ForMember(dto => dto.Latitude, ent => ent.MapFrom(p => p.Location.Y))
            .ForMember(dto => dto.Longitude, ent => ent.MapFrom(p => p.Location.X));
    }
}
