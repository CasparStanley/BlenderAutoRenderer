const RESTURL = 'http://localhost:15150/api/WineCoolers';

Vue.createApp({
    data() {
        return {
            WineCoolerData: [],
            wineCooler: {coolerId: 0, capacityOfBottles: 0, temp: 0, bottlesInStorage: 0},
            idToGetBy: 0,
            coolerToGet: 0,
            addData: {coolerId: 0, capacityOfBottles: 0, temp: 0, bottlesInStorage: 0},
            addMessage: "",
            AutoRenderers: [0]
        }
    },
    methods: {
        async getAll() {
            try {
                const response = await axios.get(RESTURL);
                this.WineCoolerData = await response.data;
            }
            catch (e) {
                alert(e.message);
            }
        },
        async get(id) {
            try {
                const url = RESTURL + "/" + id;
                const response = await axios.get(url);
                this.wineCooler = await response.data;
            }
            catch (e) {
                alert(e.message);
            }
        },
        async add() {
            try {
                response = await axios.post(RESTURL, this.addData);
                this.addMessage = "Response: " + response.status + ", " + response.statusText;
                this.getAll();
            }
            catch (e) {
                alert(e.message);
            }
        },
        async addWine(id) {
            try {
                const url = RESTURL + "/" + id;
                response = await axios.post(url);
                this.addMessage = "Response: " + response.status + ", " + response.statusText;
                this.getAll();
            }
            catch (e) {
                alert(e.message);
            }
        }
    },
    created() {
        this.getAll();

    }
}).mount("#app")