import React, { useEffect } from 'react'
import PetProfile from '../components/PetProfile'
import { useDispatch, useSelector } from 'react-redux';
import { getPets } from '../reducers/pet';

const Pets = () => {
  const dispatch = useDispatch();
  const { pets } = useSelector(state => state.pet)

  useEffect(() => {
    dispatch(getPets())
  }, [dispatch])

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
