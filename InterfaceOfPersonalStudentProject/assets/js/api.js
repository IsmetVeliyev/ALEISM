const API_URL = "http://localhost:5079";

const ApiService = {
    login: async (email, password) => {
        const response = await fetch(`${API_URL}/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password })
        });
        console.log("Login response status:", response.status);
        return await handleResponse(response);
    },

    register: async (userData) => {
        const response = await fetch(`${API_URL}/register`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(userData)
        });
        return await handleResponse(response);
    },

    getAllRooms: async (page = 1, pageSize = 6) => {
        const response = await fetch(`${API_URL}/api/Room?page=${page}&pageSize=${pageSize}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        return await handleResponse(response);
    },

    getSubRoomsByRoomId: async (roomId) => {
    const response = await fetch(`${API_URL}/api/SubRoom?roomId=${roomId}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("token")
        }
    });
    return await handleResponse(response);
   },

   joinSubRoom: async (subRoomId) => {
    const token = localStorage.getItem("token");
    const response = await fetch(`${API_URL}/join?subRoomId=${subRoomId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    });
    return await handleResponse(response);
},

leaveSubRoom: async (subRoomId) => {
    const token = localStorage.getItem("token");
    const response = await fetch(`${API_URL}/leave?subRoomId=${subRoomId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    });
    return await handleResponse(response);
},

getAllUserSubRooms: async function(subRoomId) {
        const response = await fetch(`${API_URL}/getAll?subRoomId=${subRoomId}`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem("token")}`,
                'Content-Type': 'application/json'
            }
        });
        return await handleResponse(response);
    },

    getMessagesByRoomId: async (roomId) => {
    const token = localStorage.getItem("token");
    const response = await fetch(`${API_URL}/messages/${roomId}`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    });

    if (!response.ok) {
        throw new Error('Failed to fetch messages');
    }

    const data = await response.json();
    return data.messages || [];
},


    sendMessage: async (message) => {
        const token = localStorage.getItem("token");
        const response = await fetch(`${API_URL}/sendmessage`, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
             },
          body: JSON.stringify(message)
       });
    return await handleResponse(response);
},





    enterRoom: async (roomId, password) => {
        const response = await fetch(`${API_URL}/api/Room/${roomId}/enter`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            },
            body: JSON.stringify({ password: password || "" })
        });
        return await handleResponse(response);
    },

    deleteRoom: async (id) => {
        const response = await fetch(`${API_URL}/api/Room/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        return await handleResponse(response);
    },

    getRoomById: async (id) => {
        const response = await fetch(`${API_URL}/api/Room/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        return await handleResponse(response);
    },

    updateRoom: async (id, roomData) => {
        const response = await fetch(`${API_URL}/api/Room/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            },
            body: JSON.stringify(roomData)
        });
        return await handleResponse(response);
    },

    addRoom: async (roomData) => {
        const response = await fetch(`${API_URL}/api/Room`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' ,
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            },
            body: JSON.stringify(roomData)
        });
        return await handleResponse(response);
    },

    addSubRoom: async (subRoomData) => {
        const response = await fetch(`${API_URL}/api/SubRoom`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' ,
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            },
            body: JSON.stringify(subRoomData)
        });
        return await handleResponse(response);
    },

    getSubMessagesByRoomId: async (subroomId) => {
        const token = localStorage.getItem("token");
        const response = await fetch(`${API_URL}/submessages/${subroomId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch sub-messages');
        }

        const data = await response.json();
        return data.messages || [];
    },

    sendSubMessage: async (message) => {
        const token = localStorage.getItem("token");
        const response = await fetch(`${API_URL}/sendsubmessage`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
            body: JSON.stringify(message)
        });
        return await handleResponse(response);
    },

    sendDirectMessage: async (messageDto) => {
        const token = localStorage.getItem("token");
        const response = await fetch(`${API_URL}/senddirectmessage`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
            body: JSON.stringify(messageDto)
        });
        return await handleResponse(response);
    },

    getDirectMessagesByUserId: async (userId) => {
        const token = localStorage.getItem("token");
        const response = await fetch(`${API_URL}/directmessages/${userId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            }
        });
        if (!response.ok) {
            throw new Error('Failed to fetch direct messages');
        }
        const data = await response.json();
        return data.messages || [];
    },

    getAdminStats: async () => {
        const response = await fetch(`${API_URL}/api/Admin/stats`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        return await handleResponse(response);
    },

    getAdminUsers: async () => {
        const response = await fetch(`${API_URL}/api/Admin/users`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        return await handleResponse(response);
    },

    deleteAdminUser: async (id) => {
        const response = await fetch(`${API_URL}/api/Admin/users/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        return await handleResponse(response);
    },

    deleteAdminRoom: async (id) => {
        const response = await fetch(`${API_URL}/api/Admin/rooms/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        return await handleResponse(response);
    },

    getAdminRooms: async () => {
        const response = await fetch(`${API_URL}/api/Admin/rooms`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        return await handleResponse(response);
    },

    adminAddSubRoom: async (roomId, subRoomName) => {
        const response = await fetch(`${API_URL}/api/Admin/subrooms`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            },
            body: JSON.stringify({ roomId, subRoomName })
        });
        return await handleResponse(response);
    },

    deleteSubRoom: async (id) => {
        const response = await fetch(`${API_URL}/api/SubRoom/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        return await handleResponse(response);
    },






}







async function handleResponse(response) {
    const text = await response.text();
    console.log("Sunucudan gelen ham metin:", text);

    if (response.status === 401) {
        localStorage.removeItem('token');
        window.location.href = 'Login.html';
        throw new Error("Session expired. Please login again.");
    }

    if (!response.ok) {
        const error = new Error(text || "İşlem başarısız!");
        error.status = response.status;
        throw error;
    }

    try {
        return JSON.parse(text);
    } catch (e) {
        console.error("JSON Parse hatası:", e);
        return {};
    }
}