import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import { instance } from '~/config/axiosConfig'
import TableSearch from '../../components/TableSearch'

const readOnly = [
  true,
  false,
  false,
  true,
  true
]
  

const types = [
  'number',
  'string',
  'string',
  'date',
  'date'
]
const initValues = {
  id: 0,
  sizeName: '',
  description: '',
  createdDate: '',
  updatedDate: ''
}

const actions = {
  getAll: '/Size/getALl',
  put : '/Size/update/',
  post: '/Size/add',
  delete: '/Size/delete/'
}

function Size() {

    const [data,setData] = useState([])
    const [value,setValue] = useState([])
    const [reload,setReload] = useState(false)
    const [id,sizeName,description,createdDate,updatedDtae] = value
    const  valueAction = {id,sizeName,description,createdDate,updatedDtae}
    useEffect(()=> { 
      
      instance.get(`${actions.getAll}`)
      .then( rs => {

        let temp = new Array()

        rs.data.map(({id,sizeName,description,createdDate,updatedDate}) => {
          
          temp.push({
            id,
            sizeName,
            description,
            createdDate,
            updatedDate
          })
        
        })
        setData(temp)
      })
      .catch(e => console.log(e))

    },[reload])
    const setReloaded = () => setReload(!reload)
    const lable = [
        "ID","Size name","Description","Creat date","Update date"
    ]

  return (
   <TableSearch lable={lable}  data={data} value={value} reload={reload} setReloaded={setReloaded} initValues={initValues} setValue={setValue} valueAction={valueAction}  actions = {actions} readOnly= {readOnly} types={types} />
  )
}

export default Size
