import Logo1 from '../images/vf-issuelanding-vet-checking-heartbeat-of-brown-and-white-cat.jpg'
import Logo2 from '../images/logo-white.png'
import Logo3 from '../images/2.jpg'
import Logo4 from '../images/ODAH19_med-89_-_2.webp'

import { Link } from 'react-router-dom'
import React, { useState } from 'react'
import CarouselItem from './CarouselItem'

const Carousel = () => {
  const [index, setIndex] = useState(0)

  const handlePreviousClick = () => {
    setIndex(index === 0 ? 2 : index - 1)
  }

  const handleNextClick = () => {
    setIndex(index === 2 ? 0 : index + 1)
  }

  return (
    <div id='myCarousel' className='carousel slide' data-ride='carousel'>
      <div className='carousel-indicators'>
        <button
          type='button'
          data-bs-target='#myCarousel'
          onClick={() => setIndex(0)}
          className={index === 0 ? 'active' : ''}
        ></button>
        <button
          type='button'
          data-bs-target='#myCarousel'
          onClick={() => setIndex(1)}
          className={index === 1 ? 'active' : ''}
        ></button>
        <button
          type='button'
          data-bs-target='#myCarousel'
          onClick={() => setIndex(2)}
          className={index === 2 ? 'active' : ''}
        ></button>
      </div>
      <div className='carousel-inner'>
        <CarouselItem
          isActive={index === 0}
          imgSrc={Logo1}
          header={'Happy Paws Pet Hospital'}
          description={'We care for pets when they need it most.'}
          btnText={'Join today'}
          logo={Logo2}
          linkTo={'/signin'}
        />
        <CarouselItem
          isActive={index === 1}
          imgSrc={Logo3}
          header={'Exceptional staff'}
          description={
            'We are committed to upholding the highest medical standards and are constantly searching for new and better ways to treat our patients.'
          }
          btnText={'Meet our team'}
          linkTo={'/doctors'}
        />
        <CarouselItem
          isActive={index === 2}
          imgSrc={Logo4}
          header={'Industry-leading pet care'}
          description={
            'Happy Paws stays on top of the latest advances in veterinarian technology and above all, remembers that all animals and pets need to be treated with loving care in every check-up, procedure, or surgery.'
          }
          btnText={'Learn more'}
          linkTo={'/prices'}
        />
      </div>
      <button
        className='carousel-control-prev'
        type='button'
        onClick={() => handlePreviousClick()}
      >
        <span className='carousel-control-prev-icon' aria-hidden='true'></span>
      </button>
      <button
        className='carousel-control-next'
        type='button'
        onClick={() => handleNextClick()}
      >
        <span className='carousel-control-next-icon' aria-hidden='true'></span>
      </button>
    </div>
  )
}

export default Carousel
