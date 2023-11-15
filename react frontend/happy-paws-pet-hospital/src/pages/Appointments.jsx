import React, { useState } from 'react'
import Appointment from '../components/Appointment'
import Modal from 'react-modal'
import MultiSelectDropdown from '../components/MultiSelectDropdown'

const customStyles = {
  content: {
    left: '50%',
    top: '50%',
    transform: 'translate(-50%,-50%)',
    minWidth: '25%',
    minHeight: '50%'
  }
}

const Appointments = () => {
  const [modalIsOpen, setIsOpen] = useState(false)

  function openModal () {
    setIsOpen(true)
  }

  function closeModal () {
    setIsOpen(false)
  }

  return (
    <>
      <div className='text-center mt-5'>
        <button
          type='button'
          className='btn btn-info btn-lg'
          onClick={() => openModal()}
        >
          Schedule New Appointment
        </button>
      </div>
      <Modal
        isOpen={modalIsOpen}
        onRequestClose={closeModal}
        style={customStyles}
        contentLabel='Example Modal'
      >
        <div className='modal-content'>
          <div className='modal-header'>
            <h5 class='modal-title' id='exampleModalLongTitle'>
              New Apointment
            </h5>
            <button
              type='button'
              className='btn-close'
              onClick={closeModal}
            ></button>
          </div>
          <div className='modal-body'>
            <form>
              <div class='form-group row mb-2'>
                <div class='col-sm-3'>Pet</div>
                <div class='col-sm-9'>
                  <select class='form-select'>
                    <option selected>Choose...</option>
                    <option value='1'>Nancy</option>
                    <option value='2'>Smudge</option>
                    <option value='3'>Oreo</option>
                  </select>
                </div>
              </div>
              <div class='form-group row mb-2'>
                <div class='col-sm-3'>Doctor</div>
                <div class='col-sm-9'>
                  <select class='form-select'>
                    <option selected>Choose...</option>
                    <option value='1'>Sarah Smith</option>
                    <option value='2'>Ann Jones</option>
                    <option value='3'>Liam Herwig</option>
                  </select>
                </div>
              </div>
              <MultiSelectDropdown />
              <div class='form-group row mb-2'>
                <div class='col-sm-3'>Time</div>
                <div class='col-sm-9'>
                  <select class='form-select'>
                    <option selected>Choose...</option>
                    <option value='1'>2023-10-12 12:00-13:00</option>
                    <option value='2'>2023-10-12 13:00-14:00</option>
                    <option value='3'>2023-10-12 15:30-16:30</option>
                  </select>
                </div>
              </div>
            </form>
          </div>
          <div class='modal-footer'>
            <button
              type='button'
              class='btn btn-secondary'
              onClick={closeModal}
            >
              Close
            </button>
            <button type='button' class='btn btn-info' onClick={closeModal}>
              Save changes
            </button>
          </div>
        </div>
      </Modal>
      <div className='container d-flex align-items-center'>
        <div className='row mt-3'>
          <Appointment
            time={'2023-11-25 13:00-14:00'}
            status={'Scheduled'}
            petName={'Nancy'}
            doctorName={'Sarah Smith'}
            procedures={
              'Procedures: Feline infectious peritonitis (FIP) vaccination'
            }
          />
          <Appointment
            time={'2023-10-30 10:30-11:30'}
            status={'Scheduled'}
            petName={'Oreo'}
            doctorName={'Lily Anderson'}
            procedures={'Dental extraction'}
          />
          <Appointment
            time={'2022-09-13 12:30-13:30'}
            status={'Cancelled'}
            petName={'Dave'}
            doctorName={'Liam Herwig'}
            procedures={'Wellness exam'}
          />
          <Appointment
            time={'2022-09-13 12:30-13:30'}
            status={'Cancelled'}
            petName={'Dave'}
            doctorName={'Liam Herwig'}
            procedures={'Wellness exam'}
          />
          <Appointment
            time={'2022-09-13 12:30-13:30'}
            status={'Done'}
            petName={'Smudge'}
            doctorName={'Liam Herwig'}
            procedures={'Wellness exam, Nail trim'}
          />
        </div>
      </div>
    </>
  )
}

export default Appointments
