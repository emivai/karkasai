import React from 'react'

const PetProfile = ({ imageSource, title }) => {
  return (
    <div className='d-flex flex-column col-4 my-3 align-items-center row-gap-2'>
      <img
        className='rounded-circle image'
        src={imageSource}
        width='200'
        height='200'
      />
      <h2>{title}</h2>
    </div>
  )
}

export default PetProfile
