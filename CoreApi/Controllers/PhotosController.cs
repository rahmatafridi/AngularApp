using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CoreApi.Data;
using CoreApi.Dtos;
using CoreApi.Helpers;
using CoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreApi.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController: ControllerBase
    {
       
       private readonly IAppRepository _repo;
       private readonly IMapper _mapper;
       private readonly IOptions<CloudinarySettings> _cloudinaryConfig;

       private Cloudinary _cloudinary;
       public PhotosController(IAppRepository repo, IMapper mapper, 
           IOptions<CloudinarySettings> cloudinaryConfig)
       {
          _cloudinaryConfig= cloudinaryConfig;
          _mapper= mapper;
          _repo=repo;
          Account acc =new Account(
             _cloudinaryConfig.Value.CloudName,
             _cloudinaryConfig.Value.ApiKey,
             _cloudinaryConfig.Value.ApiSecret
          );
           _cloudinary =new Cloudinary(acc);
       }

        [HttpGet("{id}",Name ="GetPhoto")]
        public async Task<ActionResult> GetPhoto(int id)
        {
            var photoFormRepo = await _repo.GetPhoto(id);
            var photo = _mapper.Map<photoForReturnDto>(photoFormRepo);
            return Ok(photo);
        }


       [HttpPost]
       public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
             if(userId !=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        return Unauthorized();
          var userForRepo = await _repo.GetUser(userId);

            var file = photoForCreationDto.File;

          //  var uploadResult = new ImageUploadResult();
          if (file.Length > 0)
          {
              using (var stream = file.OpenReadStream()){
                  var uploadParams = new ImageUploadParams()
                  {
                      File =new FileDescription(file.Name,stream),
                      Transformation =new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                  };
                   var  uploadResult =  _cloudinary.Upload(uploadParams);
                    photoForCreationDto.Url = uploadResult.Uri.ToString();
                    photoForCreationDto.PublicId = uploadResult.PublicId;
              }
          }
            var photo = _mapper.Map<Photo>(photoForCreationDto);
            if (!userForRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;
            userForRepo.Photos.Add(photo);
            var photoToRetrun = _mapper.Map<photoForReturnDto>(photo);
            if(await _repo.SaveAll())
            {
                return CreatedAtRoute("GetPhoto", new { id = photo.Id}, photoToRetrun);
           
            }
            return BadRequest("Could not add the photo");

        }

    }
}