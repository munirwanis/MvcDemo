﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dados;
using Microsoft.Ajax.Utilities;
using MvcDemo.Dto;
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class BlogPostController : Controller
    {
        private BlogDatabaseEntities db = new BlogDatabaseEntities();
        //
        // GET: /BlogPost/

        public ActionResult Index()
        {
            // Pega os posts do banco e organiza em ordem decrescente depois retorna os posts.
            var blogpostmodel = db.BlogPostSet.OrderByDescending(x => x.PostId).ToList();
            return View(blogpostmodel);
        }

        public ActionResult Create(PostDTO blogpostmodel)
        {
            // Verifica se o usuário está logado para fazer o post, se não estiver, joga pra tela de login.
            if (Session["logged"] == null) return RedirectToAction("Login", "CredentialsTest");
            else
            {
                // Cria uma variável com as informações do usuário logado.
                var user = (LoginDTO)Session["logged"];

                // Inicializa uma variável do ID do Usuário para guardar de quem foi o post.
                var UserIdLocal = 0;

                // Verifica qual foi o usuário que postou.
                foreach (var item in db.UserSet)
                {
                    if (item.UserName == user.Username)
                        UserIdLocal = item.UserId;
                }

                // Guarda as informações do post.
                var newpost = new BlogPost
                {
                    Title = blogpostmodel.Title,
                    Post = blogpostmodel.Post,
                    UserId = UserIdLocal
                };

                // Verifica se o post não está vazio, se estiver recarrega a página, se não retorna com os posts.
                if ((blogpostmodel.Post != null) &&
                    blogpostmodel.Title != null)
                {
                    db.BlogPostSet.Add(newpost);
                    db.SaveChanges();
                    return RedirectToAction("Index", "BlogPost");
                }
                else
                {
                    return View();
                }
            }
        }

        public bool NewComment(CommentDTO commentmodel)
        {
            // Se o comentário vier vazio, retorna falso e aparece um alert de erro.
            if (commentmodel.Comment == null)
            {
                return false;
            }
            // Caso contrário, verifica quem é o usuário do post, para atrelar ao comentário.
            else
            {
                foreach (var user in db.UserSet)
                {
                    if (user.UserId == commentmodel.UserId)
                    {
                        var postUserId = user.UserId;
                    }
                }
                // Também faz o atrelamento do id do post com o comentário.
                foreach (var blogPost in db.BlogPostSet)
                {
                    if (blogPost.PostId == commentmodel.PostId)
                    {
                        var blogPostId = blogPost.PostId;
                    }
                }
                // guarda os dados
                var newcomment = new Comments
                {
                    Comment = commentmodel.Comment,
                    PostId = commentmodel.PostId,
                    UserId = commentmodel.UserId
                };
                // Guarda no banco e retorna true.
                db.CommentsSet.Add(newcomment);
                db.SaveChanges();
                return true;
            }
        }

        public ActionResult ReturnComment()
        {
            //foreach (var post in db.BlogPostSet)
            //{
            //   var commentmodel = db.CommentsSet.OrderByDescending(x => x.CommentId).Select(x => x.PostId == post.PostId).ToList();
            //}
            // Apenas para teste, coloquei os comentários em ordem decrescente, mas não está retornando os comentários ainda.
            var commentmodel = db.CommentsSet.OrderByDescending(x => x.CommentId).ToList();
            return View(commentmodel);
        }

    }
}
