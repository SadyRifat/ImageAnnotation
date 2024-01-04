using ImageAnnotation.Dto;
using ImageAnnotation.Dto.Asset;
using ImageAnnotation.Models;
using ImageAnnotation.Repository;
using ImageAnnotation.Services;
using Microsoft.AspNetCore.Mvc;


namespace ImageAnnotation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController(IPhotoService photoService, IImageRepository imageRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetInfo>>> Get()
        {
            IEnumerable<AssetInfo> assets = await imageRepository.GetAll();
            var response = new BaseResponse<IEnumerable<AssetInfo>>(assets, 200, null);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<AssetInfo>>> Get(string id)
        {
            AssetInfo asset = await imageRepository.GetByIdAsync(id);
            var response = new BaseResponse<AssetInfo>(asset, 200, null);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<AssetInfo>>> Post([FromForm] AssetRequest assetRequest)
        {
            var result =  await photoService.AddPhotoAsync(assetRequest.Image);
            var asset = new AssetInfo
            {
                Id = Guid.NewGuid().ToString(),
                ImgSrc = result.Url.ToString(),
            };
            bool jobStatus = imageRepository.Add(asset);
            if (jobStatus)
            {
                var response = new BaseResponse<AssetInfo>(asset, 200, null);
                return Ok(response);
            }
            else
            {
                var errorResponse = new BaseResponse<AssetInfo>(null, 400, new ErrorResponse(400, "Unable to process"));
                return BadRequest(errorResponse);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<AssetInfo>>> Put(string id, [FromBody] AssetUpdateRequest asset)
        {
            AssetInfo assetByID = await imageRepository.GetByIdAsync(id);
            if (assetByID == null)
            {
                return BadRequest(new BaseResponse<AssetInfo>(null, 400, new ErrorResponse { Code = 400, Message = "Asset not found" }));
            }
            
            assetByID.MarkState = asset.MarkState;
            bool updateStatus = imageRepository.Update(assetByID);
            if (updateStatus)
            {
                return Ok(new BaseResponse<AssetInfo>(assetByID, 200, null));
            }
            return BadRequest(new BaseResponse<AssetInfo>(null, 400, new ErrorResponse { Code = 400, Message = "Unable to process" }));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<string>>> Delete(string id)
        {
            AssetInfo assetById = await imageRepository.GetByIdAsync(id);
            if (assetById == null)
            {
                return BadRequest(new BaseResponse<string>(null, 400, new ErrorResponse { Code = 400, Message = "Asset not found" }));
            }
            await photoService.DeletePhotoAsync(assetById.ImgSrc);
            bool deleteStatus = imageRepository.Delete(assetById);
            if (deleteStatus)
            {
                return Ok(new BaseResponse<string>("Success", 200, null));
            }
            return BadRequest(new BaseResponse<string>(null, 400, new ErrorResponse { Code = 400, Message = "Unable to process" }));
        }
    }
}
