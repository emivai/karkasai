import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { currentUser } from './reducers/auth'

export default function AuthProvider (props) {
  const dispatch = useDispatch()
  const isLoggedIn = useSelector(state => state.user.isLoggedIn)

  useEffect(() => {
    if (isLoggedIn) {
      dispatch(currentUser())
    }
  }, [isLoggedIn, dispatch])

  return <>{props.children}</>
}
