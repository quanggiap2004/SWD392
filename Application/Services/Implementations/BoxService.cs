﻿using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxDTOs;
using BlindBoxSystem.Domain.Model.BoxImageDTOs;
using BlindBoxSystem.Domain.Model.BoxItemDTOs;
using BlindBoxSystem.Domain.Model.OnlineSerieBoxDTOs;

namespace BlindBoxSystem.Application.Implementations
{
    public class BoxService : IBoxService
    {

        private readonly IBoxRepository _boxRepository;

        public BoxService(IBoxRepository boxRepository)
        {
            _boxRepository = boxRepository;
        }

        public async Task<Box> AddBoxAsync(Box box)
        {
            var addedBox = await _boxRepository.AddBoxAsync(box);
            return addedBox;
        }

        public async Task DeleteBoxAsync(int id)
        {
            var existingBox = await _boxRepository.GetBoxByIdAsync(id);

            if (existingBox != null)
            {
                await _boxRepository.DeleteBoxAsync(id);
            }
        }

        public async Task<IEnumerable<GetAllBoxesDTO>> GetAllBoxes()
        {
            var boxes = await _boxRepository.GetAllBoxesAsync();
            var boxesDTO = boxes.Select(b => new GetAllBoxesDTO
            {
                BoxId = b.BoxId,
                BoxName = b.BoxName,
                BoxDescription = b.BoxDescription,
                IsDeleted = b.IsDeleted,
                SoldQuantity = b.SoldQuantity,
                BrandId = b.BrandId,
                BrandName = b.Brand.BrandName,
                BoxImage = b.BoxImages.Select(bimage => new BoxImageDTO
                {
                    BoxId = bimage.BoxId,
                    BoxImageId = bimage.BoxImageId,
                    BoxImageUrl = bimage.BoxImageUrl,
                }).ToList(),

                BoxItem = b.BoxItems.Select(bitem => new BoxItemDTO
                {
                    BoxId = bitem.BoxId,
                    BoxItemId = bitem.BoxItemId,
                    BoxItemName = bitem.BoxItemName,
                    BoxItemColor = bitem.BoxItemColor,
                    BoxItemDescription = bitem.BoxItemDescription,
                    BoxItemEyes = bitem.BoxItemEyes,
                    AverageRating = bitem.AverageRating,
                    ImageUrl = bitem.ImageUrl,
                    NumOfVote = bitem.NumOfVote,
                    IsSecret = bitem.IsSecret,
                }).ToList(),

                OnlineSerieBox = b.OnlineSerieBoxes.Select(bOnline => new OnlineSerieBoxDTO
                {
                    BoxId = bOnline.BoxId,
                    OnlineSerieBoxId = bOnline.BoxId,
                    IsSecretOpen = bOnline.IsSecretOpen,
                    Price = bOnline.Price,
                    Name = bOnline.Name,
                    Turn = bOnline.Turn,
                }).ToList(),
            });
            return boxesDTO;
        }

        public async Task<Box> GetBoxById(int id)
        {
            var box = await _boxRepository.GetBoxByIdAsync(id);
            return box;
        }

        public async Task<GetAllBoxesDTO> GetBoxByIdDTO(int id)
        {
            var box = await _boxRepository.GetBoxByIdDTO(id);
            if (box == null)
            {
                return null;
            }

            var boxDTO = new GetAllBoxesDTO
            {
                BoxId = box.BoxId,
                BoxName = box.BoxName,
                BoxDescription = box.BoxDescription,
                IsDeleted = box.IsDeleted,
                SoldQuantity = box.SoldQuantity,
                BrandId = box.BrandId,
                BrandName = box.Brand?.BrandName,
                BoxImage = box.BoxImages?.Select(bimage => new BoxImageDTO
                {
                    BoxId = bimage.BoxId,
                    BoxImageId = bimage.BoxImageId,
                    BoxImageUrl = bimage.BoxImageUrl,
                }).ToList() ?? new List<BoxImageDTO>(),

                BoxItem = box.BoxItems?.Select(bitem => new BoxItemDTO
                {
                    BoxId = bitem.BoxId,
                    BoxItemId = bitem.BoxItemId,
                    BoxItemName = bitem.BoxItemName,
                    BoxItemColor = bitem.BoxItemColor,
                    BoxItemDescription = bitem.BoxItemDescription,
                    BoxItemEyes = bitem.BoxItemEyes,
                    AverageRating = bitem.AverageRating,
                    ImageUrl = bitem.ImageUrl,
                    NumOfVote = bitem.NumOfVote,
                    IsSecret = bitem.IsSecret,
                }).ToList() ?? new List<BoxItemDTO>(),

                OnlineSerieBox = box.OnlineSerieBoxes?.Select(bOnline => new OnlineSerieBoxDTO
                {
                    BoxId = bOnline.BoxId,
                    OnlineSerieBoxId = bOnline.BoxId,
                    IsSecretOpen = bOnline.IsSecretOpen,
                    Price = bOnline.Price,
                    Name = bOnline.Name,
                    Turn = bOnline.Turn,
                }).ToList() ?? new List<OnlineSerieBoxDTO>(),
            };
            return boxDTO;
        }

        public async Task<Box> UpdateBoxAsync(int id, Box box)
        {
            var existingBox = await _boxRepository.GetBoxByIdAsync(id);
            if (existingBox == null)
            {
                return null; // Return null if the brand does not exist
            }
            existingBox.BoxName = box.BoxName;
            existingBox.BoxDescription = box.BoxDescription;
            existingBox.BrandId = box.BrandId;

            return await _boxRepository.UpdateBoxAsync(existingBox);
        }
    }
}
