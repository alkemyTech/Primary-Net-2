import axios from "axios"

export default function handler( req, res ){
    switch(req.method){
        case "GET":
            return getAllRoles(res);

        default:
            return res.status(400).json({message:"Invalid Endpoint"})
    }
}


const getAllRoles = async(res) => {
    const { data } = await axios.get("https://localhost:7149/api/Role");
    return res.status(200).json(data)
}