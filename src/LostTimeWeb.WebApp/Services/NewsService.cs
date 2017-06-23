﻿using System;
using System.Linq;
using System.Collections.Generic;
using LostTimeDB;
using LostTimeWeb.WebApp.Models.NewsViewModels;

namespace LostTimeWeb.WebApp.Services
{
    public class NewsService
    {
        readonly  NewsGateway _newsGateway;
        public List<News> _pocoArticles; 

        public NewsService(NewsGateway newsGateway)
        {
<<<<<<< HEAD
            _studentGateway = studentGateway;
=======
            _newsGateway = newsGateway;
>>>>>>> 80f5a2debaa9e0986f9d6844c35820ba0866b725
            _pocoArticles = SetPoco();
        }
        
        public List<News> SetPoco()
        {
<<<<<<< HEAD
            //BIG POCO !!!

            List<Article> pocoArticles = new List<Article>();
=======
            List<News> pocoArticles = new List<News>();
>>>>>>> 80f5a2debaa9e0986f9d6844c35820ba0866b725

            News a1 = new News();
            a1.NewsID = 0;
            a1.NewsTitle = "Next gen of title";
            a1.NewsContent = "**Ex proident** elit ullamco consectetur tempor consectetur id. Sit aliquip deserunt nostrud excepteur occaecat commodo non dolore cupidatat est. Velit id sunt amet duis magna magna amet exercitation consequat sit nisi. Ex consequat elit culpa ullamco adipisicing reprehenderit dolore aliqua nisi proident magna mollit ad.";
            a1.NewsCreationDate = DateTime.Now;
            a1.NewsLastUpdate = DateTime.Now;
            a1.NewsAuthorID = 1;
            a1.NewsGoodVote = 1;
            a1.NewsBadVote = 31;
            a1.NewsEditionNb = 0;
            pocoArticles.Add(a1);

            News a2 = new News();
            a2.NewsID = 0;
            a2.NewsTitle = "Another title";
            a2.NewsContent = "Cillum esse ea Lorem non veniam voluptate. Culpa velit magna ullamco velit ad anim aliqua incididunt aute veniam ut. Adipisicing do ut fugiat magna ad cupidatat cupidatat qui do. Culpa exercitation veniam esse nulla ut eiusmod sint ad duis minim ipsum deserunt Lorem. Nostrud cillum labore esse ullamco do pariatur ad proident. Anim anim non dolore commodo ad commodo amet.";
            a2.NewsCreationDate = DateTime.Now;
            a2.NewsLastUpdate = DateTime.Now;
            a2.NewsAuthorID = 1;
            a2.NewsGoodVote = 25;
            a2.NewsBadVote = 1;
            a2.NewsEditionNb = 0;
            pocoArticles.Add(a2);

            return pocoArticles;
        }
<<<<<<< HEAD
       
        public Result<Article> Create( string title, int  authorId,  string content, DateTime datePost)
        {
            Article model = new Article();
            if( !IsNameValid( title ) ) return Result.Failure<Article>( Status.BadRequest, "The title is not valid." );
            if( !IsNameValid( content ) ) return Result.Failure<Article>( Status.BadRequest, "The content is not valid." );
            model.Title = title;
            model.AuthorId = authorId;
            model.Content = content;
            model.DatePost = model.DateLastEdit = datePost;
            model.ArticleId = _pocoArticles.Last().ArticleId++; 
            _pocoArticles.Add(model);
            //verifier si le contenue existe deja 
            
            return Result.Success( Status.Created, model);
        }

        public Result<Article> Update(int Id, string title, int  authorId,  string content, DateTime Article)
        {

            if( !IsNameValid( title ) ) return Result.Failure<Article>( Status.BadRequest, "The title is not valid." );
            if( !IsNameValid( content ) ) return Result.Failure<Article>( Status.BadRequest, "The content is not valid." );
            Article model = new Article();

            //if( ( student = _studentGateway.FindById( studentId ) ) == null )
            if ((model = findArticle(Id))== null)
            {
                return Result.Failure<Article>( Status.NotFound, "Article not found." );
            }
            if( title != null ) {model.title = title; }
            if( authorId != model.AuthorId ) {model.AuthorId = authorId;}
            if( content != null ) {model.Content = content}
            //if( title != null ) {model.title = title; }
            //foreach(Article toto  in _pocoArticles) { if (toto.ArticleId ==  Id) ;} a faire en rentrant.
            //_studentGateway.Update( studentId, firstName, lastName, birthDate, photo, gitHubLogin );
            return Result.Success( Status.Ok, model );
        }

        public Result<int> Delete( int Id )
        {
            Article model = new Article();
            //if( ( student = _studentGateway.FindById( studentId ) ) == null )
            //if( _studentGateway.FindById( classId ) == null ) return Result.Failure<int>( Status.NotFound, "Student not found." );
            //_studentGateway.Delete( classId );
            if ( (model = findArticle(Id))== null ) return Result.Failure<int>( Status.NotFound, "News not found." );
            _pocoArticles.Remove(model);
            return Result.Success( Status.Ok,  Id);
        }

        public Result<IEnumerable<Article>> GetAll()
        {
            IEnumerable<Article> poco = _pocoArticles;
            return Result.Success( Status.Ok, poco);
        }

        public Result<Article> GetById( int id )
        {
            Article article = findArticle(id);
            if ( article == null) return Result.Failure<Article>( Status.NotFound, "Article not found." );
            //if( ( article = _articleGateway.FindById( id ) ) == null ) return Result.Failure<Article>( Status.NotFound, "Article not found." );
            return Result.Success( Status.Ok, article );
        }

        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );

        private Article findArticle(int Id)
        {
            //if ( _pocoArticles.Exists(x => x.ArticleId == Id) == false) return null;
            foreach(Article article in _pocoArticles) { if (article.ArticleId ==  Id) return article;}
            return null;
        }

        
         /*
        public Result<Student> CreateStudent( string firstName, string lastName, DateTime birthDate, string photo, string gitHubLogin )
        {
            if( !IsNameValid( firstName ) ) return Result.Failure<Student>( Status.BadRequest, "The first name is not valid." );
            if( !IsNameValid( lastName ) ) return Result.Failure<Student>( Status.BadRequest, "The last name is not valid." );
            if( _studentGateway.FindByName( firstName, lastName ) != null ) return Result.Failure<Student>( Status.BadRequest, "A student with this name already exists." );
            if( !string.IsNullOrEmpty( gitHubLogin ) && _studentGateway.FindByGitHubLogin( gitHubLogin ) != null ) return Result.Failure<Student>( Status.BadRequest, "A student with GitHub login already exists." );
            _studentGateway.Create( firstName, lastName, birthDate, photo, gitHubLogin );
            Student student = _studentGateway.FindByName( firstName, lastName );
            return Result.Success( Status.Created, student );
        }

        public Result<Student> UpdateStudent( int studentId, string firstName, string lastName, DateTime birthDate, string photo, string gitHubLogin )
        {
            if( !IsNameValid( firstName ) ) return Result.Failure<Student>( Status.BadRequest, "The first name is not valid." );
            if( !IsNameValid( lastName ) ) return Result.Failure<Student>( Status.BadRequest, "The last name is not valid." );
            Student student;
            if( ( student = _studentGateway.FindById( studentId ) ) == null )
            {
                return Result.Failure<Student>( Status.NotFound, "Student not found." );
            }

            {
                Student s = _studentGateway.FindByName( firstName, lastName );
                if( s != null && s.StudentId != student.StudentId ) return Result.Failure<Student>( Status.BadRequest, "A student with this name already exists." );
            }

            if( !string.IsNullOrEmpty( gitHubLogin ) )
            {
                Student s = _studentGateway.FindByGitHubLogin( gitHubLogin );
                if( s != null && s.StudentId != student.StudentId ) return Result.Failure<Student>( Status.BadRequest, "A student with this GitHub login already exists." );
            }

            _studentGateway.Update( studentId, firstName, lastName, birthDate, photo, gitHubLogin );
            student = _studentGateway.FindById( studentId );
            return Result.Success( Status.Ok, student );
        }

        public Result<Student> GetById( int id )
        {
            Student student;
            if( ( student = _studentGateway.FindById( id ) ) == null ) return Result.Failure<Student>( Status.NotFound, "Student not found." );
            return Result.Success( Status.Ok, student );
        }

        public Result<int> Delete( int classId )
        {
            if( _studentGateway.FindById( classId ) == null ) return Result.Failure<int>( Status.NotFound, "Student not found." );
            _studentGateway.Delete( classId );
            return Result.Success( Status.Ok, classId );
        }
        */
=======
        public Result<News> Create(string title, int authorId, string content)
        {
            News model = new News();
            if (!IsNameValid(title)) return Result.Failure<News>(Status.BadRequest, "The title is not valid.");
            if (!IsNameValid(content)) return Result.Failure<News>(Status.BadRequest, "The content is not valid.");
            model.NewsTitle = title;
            model.NewsAuthorID = authorId;
            model.NewsContent = content;
            model.NewsCreationDate = model.NewsLastUpdate = DateTime.Now;
            model.NewsID = _pocoArticles.Last().NewsID++;
            _pocoArticles.Add(model);
            //verifier si le contenue existe deja 

            return Result.Success(Status.Created, model);
        }
        public Result<IEnumerable<News>> GetAll()
        {
            //BIG POCO !!!
            IEnumerable<News> poco = _pocoArticles;
            return Result.Success(Status.Ok, poco);
        }
        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
>>>>>>> 80f5a2debaa9e0986f9d6844c35820ba0866b725
    }
}
