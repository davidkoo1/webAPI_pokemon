﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dto;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository,
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokemonId)
        {
            if (!_pokemonRepository.PokemonExist(pokemonId))
                return NotFound();

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokemonId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("{pokemonId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokemonId)
        {
            if(!_pokemonRepository.PokemonExist(pokemonId))
                return NotFound();

            var rating = _pokemonRepository.GetPokemonRating(pokemonId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemons = _pokemonRepository.GetPokemons().Where(o => o.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                                                              .FirstOrDefault();

            if (pokemons != null)
            {
                ModelState.AddModelError("", "Pokemon alredy exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);



            if (!_pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int pokemonId,[FromQuery]int ownerId, 
                                           [FromQuery]int categoryId, 
                                           [FromBody] PokemonDto updatePokemon)
        {
            if (updatePokemon == null)
                return BadRequest(ModelState);
            if (pokemonId != updatePokemon.Id)
                return BadRequest(ModelState);
            if (!_pokemonRepository.PokemonExist(pokemonId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(updatePokemon);

            if (!_pokemonRepository.UpdatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong updating pokemon");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
