import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import { instance } from '../config/axiosConfig'
let roleDefault = '';
function RoleFilter(props) {
    const {roleId,setRoleId,addRole,setAddRole} = props
    const [roles,setRoles] = useState([])
    useEffect(()=>{
        instance.get('/Roles').then(rs => {
                setRoles(rs.data)
                roleDefault = rs.data[0].id
            }
            ).catch(e=>console.log(e))
    },[])
  return (
    <>
        <label>Edit</label>
        <input checked={addRole} onChange={()=> {setAddRole(); setRoleId(roles[0].id)}} className='check-role' type="checkbox" id="switch" /><label className='edit-role-label' for="switch">Edit</label>

        <select value={roleId !==0 ? roleId : roleDefault} onChange={(e)=> setRoleId(e.target.value)} className='role-select' >
            { roles.map((v,i) => 
            <option  key={v} value={v.id}>
                {v.name}
            </option>
            )}
        </select>
    </>
  )
}

export default RoleFilter
