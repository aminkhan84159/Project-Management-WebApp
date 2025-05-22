export interface ITask{
    taskId: number,
    taskName: string,
    description:string,
    priority: string,
    type: string,
    startDate:Date,
    endDate: Date,
    statusId: number,
    userId: number,
    projectId: number,
    isActive: true,
}
