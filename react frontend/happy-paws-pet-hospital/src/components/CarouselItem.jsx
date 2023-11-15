import React from 'react'
import { Link } from 'react-router-dom'

const CarouselItem = ({
  isActive,
  imgSrc,
  header,
  description,
  btnText,
  logo,
  linkTo
}) => {
  return (
    <div className={`carousel-item ${isActive ? 'active' : ''}`}>
      <img
        className='third-slide image darken'
        src={imgSrc}
        alt='Third slide'
      />
      <div className='container'>
        <div className='carousel-caption text-right'>
          {logo && (
            <img className='mb-4' src={logo} alt='' width='200' height='200' />
          )}
          <h1>{header}</h1>
          <p>{description}</p>
          <p>
            <Link to={linkTo}>
              <button className='btn btn-lg btn-info'>{btnText}</button>
            </Link>
          </p>
        </div>
      </div>
    </div>
  )
}

export default CarouselItem
