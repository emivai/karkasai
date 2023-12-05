import { useDispatch, useSelector } from 'react-redux'
import { createProcedure, getProcedures } from '../reducers/procedure'

const Prices = () => {
  //   const dispatch = useDispatch()
  //   const { procedures } = useSelector(state => state.procedure)

  //   useEffect(() => {
  //     dispatch(getProcedures())
  //   }, [dispatch])

  return (
    <div className='container marketing pt-5 d-flex flex-column'>
      <div className='mb-5 mx-auto align-self-start'>
        <button className='btn btn-info btn-lg'>Add procedure</button>
      </div>
    </div>
  )
}

export default Prices
