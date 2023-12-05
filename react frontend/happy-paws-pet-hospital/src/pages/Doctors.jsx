import React, { useEffect } from 'react'
import CirclePicture from '../components/CirclePicture'
import { useDispatch, useSelector } from 'react-redux';
import { getDoctors } from '../reducers/user';

const Doctors = () => {
  const dispatch = useDispatch();
  const {doctors} = useSelector(state => state.user)

  useEffect(() => {
    dispatch(getDoctors())
  }, [dispatch])

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
