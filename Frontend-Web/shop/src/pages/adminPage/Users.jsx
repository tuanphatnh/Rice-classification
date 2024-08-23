import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import TableSearch from '../../components/TableSearch'
import { instance } from '../../config/axiosConfig'
const readOnly = [
  true,
  false,
  false,
  false,
  false
  
]
const buttonActive = [true,false,false,true]
const types = [
  'string',
  'image',
  'string',
  'string',
  'profile'
]
const initValues = {
  id : '',
  avatarUrl : '',
  username : "",
  email: '',
  
}
const lable = [
  "ID","Avartar","Username",'Email',"Action"
]

const actions = {
  getALl: '/User/getALl',
  getRole: "/Roles/getRoleByUser",
  getProfile: '/user/profile'
}
function User() {
  const [data,setData] = useState([])
  const [value,setValue] = useState([])
  const [reload,setReload] = useState(false)
  const [id,avatarUrl,username,email] = value
  const  valueAction = {id,avatarUrl,username,email}
  useEffect(()=> { 

      instance.get(actions.getALl)
      .then( rs => {
  
        let temp = new Array()
        
        rs.data.map(({id,avatarUrl,username,email}) => {
          let profile = `${id}`;
  
  
          
          
           temp.push({
            id,
            avatarUrl,
            username,
            email,
            profile,
          })
        })
        setData(temp)
      })
      .catch(e => console.log(e))
    

  },[reload])
  const setReloaded = () => setReload(!reload)
 

return (
 <TableSearch lable={lable} buttonActive={buttonActive}  data={data} value={value} reload={reload} setReloaded={setReloaded} initValues={initValues} setValue={setValue} valueAction={valueAction}  actions = {actions} readOnly= {readOnly} types={types} />
)
}

export default User
