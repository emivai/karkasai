import React, { useEffect, useState } from 'react'
import CirclePicture from '../components/CirclePicture'
import lilyAnderson from '../images/istockphoto-1330046035-612x612.jpg'
import davidWilliams from '../images/Dr.-Snead-e1678809328573.jpeg'
import sarahSmith from '../images/istockphoto-1136590917-640x640.jpg'
import annJones from '../images/depositphotos_389739044-stock-photo-profile-picture-of-female-doctor.jpg'
import bellaMoore from '../images/justinelee.jpg'
import liamHerwing from '../images/63ecfd25c727f_Lemon.png.webp'
import axios from 'axios'

const Doctors = () => {
  useEffect(() => {
    const fetchData = async () => {
      const data = await fetch('https://localhost:7294/users?type=2')
      const json = await data.json()
      setDoctors(json)
    }
    fetchData().catch(console.error)
  }, [])

  const [doctors, setDoctors] = useState()

  return (
    <div className='container marketing pt-5 d-flex flex-column'>
      <div className='row'>
        {doctors?.map(doctor => (
          <CirclePicture
            key={doctor.id}
            title={doctor.name + ' ' + doctor.surname}
            imageSource={doctor.photo}
          />
        ))}
      </div>
    </div>
  )
}

export default Doctors
