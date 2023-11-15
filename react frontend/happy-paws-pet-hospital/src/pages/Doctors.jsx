import React, { useEffect, useState } from 'react'
import CirclePicture from '../components/CirclePicture'

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
