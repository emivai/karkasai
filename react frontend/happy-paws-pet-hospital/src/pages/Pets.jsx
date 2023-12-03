import React, { useEffect, useState } from 'react'
import PetProfile from '../components/PetProfile'

const Pets = () => {
  useEffect(() => {
    const fetchData = async () => {
      // const userResponse = await fetch(
      //   'https://localhost:7294/auth/currentUser'
      // )
      //const userJson = await userResponse.json()
      //const token = userJson.token
      const data = await fetch('https://localhost:7294/pets')

      const json = await data.json()
      setPets(json)
    }
    fetchData().catch(console.error)
  }, [])

  const [pets, setPets] = useState()

  return (
    <div className='container marketing pt-5 d-flex flex-column'>
      <div className='row'>
        {pets?.map(pet => (
          <PetProfile key={pet.id} title={pet.name} imageSource={pet.photo} />
        ))}
      </div>
    </div>
  )
}

export default Pets
