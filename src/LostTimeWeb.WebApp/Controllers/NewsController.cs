﻿using System.Collections.Generic;
using System.Linq;
using LostTimeWeb.DAL;
using LostTimeWeb.WebApp.Authentication;
using LostTimeWeb.WebApp.Models.NewsViewModels;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LostTimeWeb.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    //[Authorize( ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class NewsController : Controller
    {
        readonly NewsService _newsServices;

        public NewsController(NewsService newsServices)
        {
            _newsServices = newsServices;
        }

        [HttpGet]
        public IActionResult GetNewsList()
        {
            Console.WriteLine("NEWSLIST CALLED");
            Result<IEnumerable<Article>> result = _newsServices.GetAll();
            return this.CreateResult<IEnumerable<Article>, IEnumerable<ArticleViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToArticleViewModel() );
            } );
        }

        [HttpPost]
        public IActionResult Create( [FromBody] ArticleViewModel model )
        {
            Result<Article> result = _newsServices.Create( model.Title, model.AuthorId, model.Content, model.DatePost); 
            return this.CreateResult<Article , ArticleViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToArticleViewModel();
                o.RouteName = "GetArticle";
                o.RouteValues = s => new { id = s.ArticleId };
            } );
        }

        [HttpPut( "{id}" )]
        public IActionResult Update( int id, [FromBody] ArticleViewModel model )
        {
            Result<Article> result = _newsServices.Update(id, model.Title, model.AuthorId, model.Content, model.DatePost);
            return this.CreateResult<Article, ArticleViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToArticleViewModel();
            } );
        }

        [HttpDelete( "{id}" )]
        public IActionResult Delete( int id )
        {
            Result<int> result =  _newsServices.Delete( id );
            return this.CreateResult( result );
        }

        /*
        [HttpGet( "{id}", Name = "GetStudent" )]
        public IActionResult GetStudentById( int id )
        {
            Result<Student> result = _studentService.GetById( id );
            return this.CreateResult<Student, StudentViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToStudentViewModel();
            } );
        }

        [HttpPost]
        public IActionResult CreateStudent( [FromBody] StudentViewModel model )
        {
            Result<Student> result = _studentService.CreateStudent( model.FirstName, model.LastName, model.BirthDate, model.Photo, model.GitHubLogin );
            return this.CreateResult<Student, StudentViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToStudentViewModel();
                o.RouteName = "GetStudent";
                o.RouteValues = s => new { id = s.StudentId };
            } );
        }

        [HttpPut( "{id}" )]
        public IActionResult UpdateStudent( int id, [FromBody] StudentViewModel model )
        {
            Result<Student> result = _studentService.UpdateStudent( id, model.FirstName, model.LastName, model.BirthDate, model.Photo, model.GitHubLogin );
            return this.CreateResult<Student, StudentViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToStudentViewModel();
            } );
        }

        [HttpDelete( "{id}" )]
        public IActionResult DeleteStudent( int id )
        {
            Result<int> result = _studentService.Delete( id );
            return this.CreateResult( result );
        }
        */
    }
}