using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Categories;
using Api_Elearning.DTOs.Courses;
using Api_Elearning.DTOs.Informations;
using Api_Elearning.DTOs.Instructors;
using Api_Elearning.DTOs.Sliders;
using Api_Elearning.Models;
using AutoMapper;
using Microsoft.VisualBasic;
using Information = Api_Elearning.Models.Information;

namespace Api_Elearning.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Slider, SliderDto>();
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<SliderEditDto, Slider>();

            CreateMap<About, AboutDto>();
            CreateMap<AboutCreateDto, About>();
            CreateMap<AboutEditDto, About>();

            CreateMap<Information, InformationDto>();
            CreateMap<InformtationCreateDto, Information>();
            CreateMap<InformationEditDto, Information>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryEditDto, Category>();

            CreateMap<Course, CourseDto>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Instructor, opt => opt.MapFrom(s => s.Instructor.FullName))
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.CourseImages.Select(i => new CourseImageDto
                {
                    Name = i.Name,
                    IsMain = i.IsMain
                }).ToList()));
            CreateMap<Course, CourseImageDto>();
            CreateMap<Course, DeleteAndMainDto>();
            CreateMap<CourseCreateDto, Course>();
            CreateMap<CourseEditDto, Course>();

            CreateMap<Instructor, InstructorDto>();
            CreateMap<InstructorCreateDto, Instructor>();
            CreateMap<InstructorEditDto, Instructor>();


        }
    }
}
