import React from 'react'
import { PieChart, Pie, Sector, Cell, ResponsiveContainer, Tooltip } from 'recharts';

const data = [
  { name: 'Group A', value: 400 },
  { name: 'Group B', value: 300 },
  { name: 'Group C', value: 300 },
  { name: 'Group D', value: 200 },
];
const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];

function PieChartSingle(props) {
    const {nameChart} = props
  return (
   <>
   <div className='chart'>
    <h3>{nameChart}</h3>
    <PieChart height={300} width={300}>
        <Pie
          data={data}
          cx={130}
          cy={130}
          innerRadius={100}
          outerRadius={120}
          fill="#8884d8"
          paddingAngle={5}
          dataKey="value"
        >
          {data.map((entry, index) => (
            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
          ))}
        </Pie>
      <Tooltip />
      </PieChart>
   </div>
 
   </>
  )
}

export default PieChartSingle
